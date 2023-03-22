﻿using System;
using System.IO;
using System.Net;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;

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

        private byte[] FileHeader;

        public BinaryPacketReader(SniffType type, string fileName, Encoding encoding)
        {
            _sniffType = type;
            _reader = new BinaryReader(new FileStream(@fileName, FileMode.Open, FileAccess.Read, FileShare.Read), encoding);
            ReadHeader();
            StoreFileHeader();
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
                    SetLocale(Encoding.ASCII.GetString(_reader.ReadBytes(4)));              // client locale
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

                        if (_snifferVersion >= 0x0107)
                            _startTime = DateTime.FromFileTimeUtc(BitConverter.ToInt64(optionalData, 3));
                    }
                    else if (_snifferId == 0x15 || _snifferId == 0x16) // ymir
                    {
                        if (additionalLength >= 2)
                            _snifferVersion = BitConverter.ToInt16(optionalData, 0);
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

        void StoreFileHeader()
        {
            // all data till this position belong to the sniffFile header
            // store the end position of the header
            // then reset it to 0 and simply store the whole header again as byte[]
            var currentPosition = _reader.BaseStream.Position;
            _reader.BaseStream.Position = 0;
            FileHeader = _reader.ReadBytes((int)currentPosition);
        }

        static void SetBuild(uint build)
        {
            ClientVersion.SetVersion((ClientVersionBuild)build);
        }

        static void SetLocale(string locale)
        {
            ClientLocale.SetLocale(locale);
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
            StringBuilder writer = null;
            int cIndex = 0;
            byte[] header;
            IPEndPoint endPoint = null; // Only used in PKT3.1 by TC's PacketLogger

            // Note: PacketHeader ends before data
            var packetHeaderStartPosition = _reader.BaseStream.Position;
            long packetHeaderEndPosition = 0;

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
                            packetHeaderEndPosition = _reader.BaseStream.Position;
                            data = _reader.ReadBytes(length - 2);
                        }
                        else
                        {
                            opcode = _reader.ReadInt32();
                            packetHeaderEndPosition = _reader.BaseStream.Position;
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
                            time = DateTime.SpecifyKind(time, DateTimeKind.Utc);
                            time = TimeZoneInfo.ConvertTimeFromUtc(time, TimeZoneInfo.Local);
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
                        else if ((_snifferId == 0x15 || _snifferId == 0x16) && additionalSize > 0) // ymir
                        {
                            var optionalStart = _reader.BaseStream.Position;
                            var unixMilliseconds = _reader.ReadDouble();
                            time = DateTime.UnixEpoch.AddMilliseconds(unixMilliseconds);
                            time = DateTime.SpecifyKind(time, DateTimeKind.Utc);
                            time = TimeZoneInfo.ConvertTimeFromUtc(time, TimeZoneInfo.Local);
                            if (_snifferVersion >= 0x101)
                            {
                                var commentLength = _reader.ReadByte();
                                if (commentLength > 0)
                                {
                                    if (Settings.DumpFormatWithText())
                                    {
                                        writer = new StringBuilder();
                                        writer.AppendLine("# " + Encoding.UTF8.GetString(_reader.ReadBytes(commentLength)));
                                        writer.AppendLine();
                                    }
                                }
                            }

                            if (_snifferVersion >= 0x0102)
                            {
                                while (_reader.BaseStream.Position - optionalStart < additionalSize)
                                {
                                    var type = _reader.ReadByte();
                                    switch (type)
                                    {
                                        case 0x50: // override phasing
                                        {
                                            var lowGuid = _reader.ReadUInt64();
                                            var highGuid = _reader.ReadUInt64();
                                            var phaseId = _reader.ReadInt32();
                                            var affectedGuid = new WowGuid128(lowGuid, highGuid);
                                            if (Storage.Objects.ContainsKey(affectedGuid))
                                                Storage.Objects[affectedGuid].Item1.PhaseOverride = phaseId;
                                            break;
                                        }
                                        default:
                                        {
                                            _reader.ReadBytes((int)(_reader.BaseStream.Position - optionalStart)); // skip unk
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                            _reader.ReadBytes(additionalSize);

                        opcode = _reader.ReadInt32();
                        packetHeaderEndPosition = _reader.BaseStream.Position;
                        data = _reader.ReadBytes(length - 4);
                        break;
                    }
                    default:
                    {
                        opcode = _reader.ReadUInt16();
                        length = _reader.ReadInt32();
                        direction = (Direction)_reader.ReadByte();
                        time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt64());
                        packetHeaderEndPosition = _reader.BaseStream.Position;
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
                packetHeaderEndPosition = _reader.BaseStream.Position;
                data = _reader.ReadBytes(length);
            }

            if (number == 0)
            {
                // determine build version based on date of first packet if not specified otherwise
                if (ClientVersion.IsUndefined())
                    ClientVersion.SetVersion(time);
            }

            // store the end position of the packet so we can return here
            var packetEndPosition = _reader.BaseStream.Position;

            // go back to packetHeaderStartPosition
            _reader.BaseStream.Position = packetHeaderStartPosition;
            // store all bytes till packetHeaderEndPosition, because its the header
            header = _reader.ReadBytes((int)(packetHeaderEndPosition - packetHeaderStartPosition));
            // go back to end of packet
            _reader.BaseStream.Position = packetEndPosition;
            // ignore opcodes that were not "decrypted" (usually because of
            // a missing session key) (only applicable to 335 or earlier)
            if (opcode >= 1312 && (ClientVersion.Build <= ClientVersionBuild.V3_3_5a_12340 && ClientVersion.Build != ClientVersionBuild.Zero))
                return null;

            return new Packet(data, opcode, time, direction, number, writer, Path.GetFileName(fileName))
            {
                ConnectionIndex = cIndex,
                EndPoint = endPoint,
                Header = header
            };
        }

        public long GetTotalSize()
        {
            return _reader?.BaseStream.Length ?? 0;
        }

        public long GetCurrentSize()
        {
            return _reader?.BaseStream.Position ?? 0;
        }

        public byte[] GetFileHeader()
        {
            return FileHeader;
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
