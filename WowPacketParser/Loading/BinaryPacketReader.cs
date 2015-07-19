using System;
using System.IO;
using System.Net;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public sealed class BinaryPacketReader : IPacketReader
    {
        enum PktVersion
        {
            NoHeader = 0,
// ReSharper disable InconsistentNaming
            V2_1 = 0x201,
            V2_2 = 0x202,
            V3_0 = 0x300,
            V3_1 = 0x301
// ReSharper restore InconsistentNaming
        }

        private BinaryReader _reader;

        private readonly SniffType _sniffType;
        private PktVersion _pktVersion;

        private DateTime _startTime;
        private uint _startTickCount;
        private int _snifferId;
        private short _snifferVersion;
        private static string _locale;

        public BinaryPacketReader(SniffType type, string fileName, Encoding encoding)
        {
            _sniffType = type;
            _reader = new BinaryReader(new FileStream(@fileName, FileMode.Open, FileAccess.Read, FileShare.Read), encoding);
            ReadHeader();
        }

        void ReadHeader()
        {
            var headerStart = _reader.ReadBytes(3);             // PKT
            if (Encoding.ASCII.GetString(headerStart) != "PKT")
            {
                // file does not have a header
                _reader.BaseStream.Position = 0;
                return;
            }

            _pktVersion = (PktVersion)_reader.ReadUInt16();      // sniff version

            int additionalLength;

            switch (_pktVersion)
            {
                case PktVersion.V2_1:
                {
                    SetBuild(_reader.ReadUInt16()); // client build
                    _reader.ReadBytes(40); // session key
                    break;
                }
                case PktVersion.V2_2:
                {
                    _snifferId = _reader.ReadByte();            // sniffer id
                    SetBuild(_reader.ReadUInt16());             // client build
                    _reader.ReadBytes(4);                       // client locale
                    _reader.ReadBytes(20);                      // packet key
                    _reader.ReadBytes(64);                      // realm name
                    break;
                }
                case PktVersion.V3_0:
                {
                    _snifferId = _reader.ReadByte();            // sniffer id
                    SetBuild(_reader.ReadUInt32());             // client build
                    _reader.ReadBytes(4);                       // client locale
                    _reader.ReadBytes(40);                      // session key
                    additionalLength = _reader.ReadInt32();
                    var preAdditionalPos = _reader.BaseStream.Position;
                    _reader.ReadBytes(additionalLength);
                    var postAdditionalPos = _reader.BaseStream.Position;
                    if (_snifferId == 10)                       // xyla
                    {
                        _reader.BaseStream.Position = preAdditionalPos;
                        _startTime = Utilities.GetDateTimeFromUnixTime(_reader.ReadUInt32());   // start time
                        _startTickCount = _reader.ReadUInt32(); // start tick count
                        _reader.BaseStream.Position = postAdditionalPos;
                    }
                    break;
                }
                case PktVersion.V3_1:
                {
                    _snifferId = _reader.ReadByte();                                        // sniffer id
                    SetBuild(_reader.ReadUInt32());                                         // client build
                    _locale = Encoding.ASCII.GetString(_reader.ReadBytes(4));               // client locale
                    _reader.ReadBytes(40);                                                  // session key
                    _startTime = Utilities.GetDateTimeFromUnixTime(_reader.ReadUInt32());   // start time
                    _startTickCount = _reader.ReadUInt32();                                 // start tick count
                    additionalLength = _reader.ReadInt32();
                    var optionalData = _reader.ReadBytes(additionalLength);
                    if (_snifferId == 'S') // WSTC
                    {
                        // versions 1.5 and older store human readable sniffer description string in header
                        // version 1.6 adds 3 bytes before that data, 0xFF separator, one byte for major version and one byte for minor version, expecting 0x0106 for 1.6
                        if (additionalLength >= 3 && optionalData[0] == 0xFF)
                            _snifferVersion = BitConverter.ToInt16(optionalData, 1);
                        else
                            _snifferVersion = 0x0105;
                    }
                    break;
                }
                default:
                {
                    // not supported version - let's assume the PKT bytes were a coincidence
                    _reader.BaseStream.Position = 0;
                    break;
                }
            }
        }

        static void SetBuild(uint build)
        {
            ClientVersion.SetVersion((ClientVersionBuild)build);
        }

        public static string GetClientLocale()
        {
            return _locale;
        }

        public static LocaleConstant GetLocale()
        {
            return (LocaleConstant) Enum.Parse(typeof(LocaleConstant), _locale);
        }

        public bool CanRead()
        {
            return _reader.BaseStream.Position != _reader.BaseStream.Length;
        }

        public Packet Read(int number, string fileName)
        {
            int opcode;
            int length;
            DateTime time;
            Direction direction;
            byte[] data;
            int cIndex = 0;
            IPEndPoint endPoint = null; // Only used in PKT3.1 by TC's PacketLogger

            if (_sniffType == SniffType.Pkt)
            {
                switch (_pktVersion)
                {
                    case PktVersion.V2_1:
                    case PktVersion.V2_2:
                    {
                        direction = (_reader.ReadByte() == 0xff) ? Direction.ServerToClient : Direction.ClientToServer;
                        time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
                        _reader.ReadInt32(); // tick count
                        length = _reader.ReadInt32();

                        if (direction == Direction.ServerToClient)
                        {
                            opcode = _reader.ReadInt16();
                            data = _reader.ReadBytes(length - 2);
                        }
                        else
                        {
                            opcode = _reader.ReadInt32();
                            data = _reader.ReadBytes(length - 4);
                        }

                        break;
                    }
                    case PktVersion.V3_0:
                    case PktVersion.V3_1:
                    {
                        switch (_reader.ReadUInt32())
                        {
                            case 0x47534d53:
                                direction = Direction.ServerToClient;
                                break;
                            case 0x47534d43:
                                direction = Direction.ClientToServer;
                                break;
                            case 0x4e425f53:
                                direction = Direction.BNServerToClient;
                                break;
                            default:
                                direction = Direction.BNClientToServer;
                                break;
                        }

                        if (_pktVersion == PktVersion.V3_0)
                        {
                            time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
                            var tickCount = _reader.ReadUInt32();
                            if (_startTickCount != 0)
                                time = _startTime.AddMilliseconds(tickCount - _startTickCount);
                        }
                        else
                        {
                            cIndex = _reader.ReadInt32(); // session id, connection index
                            var tickCount = _reader.ReadUInt32();
                            time = _startTime.AddMilliseconds(tickCount - _startTickCount);
                        }

                        int additionalSize = _reader.ReadInt32();
                        length = _reader.ReadInt32();
                        if (additionalSize >= 20 && _snifferId == 'T' && _pktVersion == PktVersion.V3_1)
                        {
                            // TC's PacketLogger - extract socket UUID
                            // (16 bytes address and 4 bytes of port)
                            var realIpBytes = _reader.ReadBytes(16);
                            var port = _reader.ReadInt32();

                            var firstPartIpBytes = new byte[4];
                            var secondPartIpBytes = new byte[12];

                            Array.Copy(realIpBytes, 0, firstPartIpBytes, 0, 4);
                            Array.Copy(realIpBytes, 4, secondPartIpBytes, 0, 12);

                            endPoint = new IPEndPoint(new IPAddress(Array.TrueForAll(secondPartIpBytes, b => b == 0) ?
                                firstPartIpBytes : realIpBytes), port);

                            _reader.ReadBytes(additionalSize - 20);
                        }
                        else
                            _reader.ReadBytes(additionalSize);

                        opcode = _reader.ReadInt32();
                        data = _reader.ReadBytes(length - 4);
                        break;
                    }
                    default:
                    {
                        opcode = _reader.ReadUInt16();
                        length = _reader.ReadInt32();
                        direction = (Direction)_reader.ReadByte();
                        time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt64());
                        data = _reader.ReadBytes(length);
                        break;
                    }
                }
            }
            else // bin
            {
                opcode = _reader.ReadInt32();
                length = _reader.ReadInt32();
                time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
                direction = (Direction)_reader.ReadByte();
                data = _reader.ReadBytes(length);
            }

            // ignore opcodes that were not "decrypted" (usually because of
            // a missing session key) (only applicable to 335 or earlier)
            if (opcode >= 1312 && (ClientVersion.Build <= ClientVersionBuild.V3_3_5a_12340 && ClientVersion.Build != ClientVersionBuild.Zero))
                return null;

            return new Packet(data, opcode, time, direction, number, Path.GetFileName(fileName))
            {
                ConnectionIndex = cIndex,
                EndPoint = endPoint
            };
        }

        public long GetTotalSize()
        {
            return _reader != null ? _reader.BaseStream.Length : 0;
        }

        public long GetCurrentSize()
        {
            return _reader != null ? _reader.BaseStream.Position : 0;
        }

        public void Dispose()
        {
            if (_reader == null) return;
            _reader.BaseStream.Dispose();
            _reader.Dispose();
            _reader = null;
        }
    }
}
