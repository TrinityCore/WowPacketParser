using System;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public class BinaryPacketReader : IPacketReader
    {
        enum PktVersion
        {
            NoHeader = 0,
            V2_1 = 0x201,
            V2_2 = 0x202,
            V3_0 = 0x300,
            V3_1 = 0x301,
        }

        private readonly BinaryReader _reader;

        private readonly SniffType _sniffType;
        private PktVersion pktVersion = PktVersion.NoHeader;

        private DateTime _startTime;
        private uint _startTickCount;

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
                // pkt does not have a header
                _reader.BaseStream.Position = 0;
                return;
            }

            pktVersion = (PktVersion)_reader.ReadUInt16();      // sniff version

            int additionalLength;

            switch (pktVersion)
            {
                case PktVersion.V2_1:
                    SetBuild(_reader.ReadUInt16());             // client build
                    _reader.ReadBytes(40);                      // session key
                    break;
                case PktVersion.V2_2:
                    _reader.ReadByte();                         // sniffer id
                    SetBuild(_reader.ReadUInt16());             // client build
                    _reader.ReadBytes(4);                       // client locale
                    _reader.ReadBytes(20);                      // packet key
                    _reader.ReadBytes(64);                      // realm name
                    break;
                case PktVersion.V3_0:
                    _reader.ReadByte();                         // sniffer id
                    SetBuild(_reader.ReadUInt32());             // client build
                    _reader.ReadBytes(4);                       // client locale
                    _reader.ReadBytes(40);                      // session key
                    additionalLength = _reader.ReadInt32();
                    _reader.ReadBytes(additionalLength);
                    break;
                case PktVersion.V3_1:
                    _reader.ReadByte();                         // sniffer id
                    SetBuild(_reader.ReadUInt32());             // client build
                    _reader.ReadBytes(4);                       // client locale
                    _reader.ReadBytes(40);                      // session key
                    _startTime = Utilities.GetDateTimeFromUnixTime(_reader.ReadUInt32()); // start time
                    _startTickCount = _reader.ReadUInt32();     // start tick count
                    additionalLength = _reader.ReadInt32();
                    _reader.ReadBytes(additionalLength);
                    break;
                default:
                    // not supported version - let's assume the PKT bytes were a coincidence
                    _reader.BaseStream.Position = 0;
                    break;
            }
        }

        void SetBuild(uint build)
        {
            if (ClientVersion.IsUndefined())
                ClientVersion.SetVersion((ClientVersionBuild)build);
        }

        public bool CanRead()
        {
            return _reader.BaseStream.Position != _reader.BaseStream.Length;
        }

        public Packet Read(int number)
        {
            int opcode;
            int length;
            DateTime time;
            Direction direction;
            byte[] data;

            if (_sniffType == SniffType.Pkt)
            {
                switch (pktVersion)
                {
                    case PktVersion.V2_1:
                    case PktVersion.V2_2:
                        direction = (_reader.ReadByte() == 0xff) ? Direction.ServerToClient : Direction.ClientToServer;
                        time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
                        _reader.ReadInt32(); // tick count
                        length = _reader.ReadInt32();

                        if (direction == Direction.ServerToClient)
                        {
                            opcode = (int)_reader.ReadInt16();
                            data = _reader.ReadBytes(length - 2);
                        }
                        else
                        {
                            opcode = _reader.ReadInt32();
                            data = _reader.ReadBytes(length - 4);
                        }

                        break;
                    case PktVersion.V3_0:
                    case PktVersion.V3_1:
                        direction = (_reader.ReadUInt32() == 0x47534d53) ? Direction.ServerToClient : Direction.ClientToServer;

                        if (pktVersion == PktVersion.V3_0)
                        {
                            time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
                            var tickCount = _reader.ReadUInt32();
                            time = time.AddMilliseconds(tickCount);
                        }
                        else
                        {
                            _reader.ReadUInt32(); // session id
                            var tickCount = _reader.ReadUInt32();
                            time = _startTime.AddMilliseconds(tickCount - _startTickCount);
                        }

                        int additionalSize = _reader.ReadInt32();
                        length = _reader.ReadInt32();
                        _reader.ReadBytes(additionalSize);
                        opcode = _reader.ReadInt32();
                        data = _reader.ReadBytes(length - 4);
                        break;
                    default:
                        opcode = _reader.ReadUInt16();
                        length = _reader.ReadInt32();
                        direction = (Direction)_reader.ReadByte();
                        time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt64());
                        data = _reader.ReadBytes(length);
                        break;
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

            return new Packet(data, opcode, time, direction, number);
        }

        public void Close()
        {
            if (_reader != null)
                _reader.Close();
        }
    }
}
