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

        public BinaryPacketReader(SniffType type, string fileName, Encoding encoding)
        {
            _sniffType = type;
            _reader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), encoding);

            ReadHeader();
        }

        void ReadHeader()
        {
            var headerStart = _reader.ReadBytes(3);            // PKT
            if (Encoding.ASCII.GetString(headerStart) != "PKT")
            {
                // pkt does not have a header
                _reader.BaseStream.Position = 0;
                return;
            }

            pktVersion = (PktVersion)_reader.ReadUInt16();    // sniff version

            int additionalLength;

            switch (pktVersion)
            {
                case PktVersion.V2_1:
                    SetBuild(_reader.ReadUInt16());            // client build
                    _reader.ReadBytes(40);                    // session key
                    break;
                case PktVersion.V2_2:
                    _reader.ReadByte();                        // sniffer id
                    SetBuild(_reader.ReadUInt16());            // client build
                    _reader.ReadBytes(4);                    // client locale
                    _reader.ReadBytes(20);                    // packet key
                    _reader.ReadBytes(64);                    // realm name
                    break;
                case PktVersion.V3_0:
                    _reader.ReadByte();                        // sniffer id
                    SetBuild(_reader.ReadInt32());            // client build
                    _reader.ReadBytes(4);                    // client locale
                    _reader.ReadBytes(40);                    // session key
                    additionalLength = _reader.ReadInt32();
                    _reader.ReadBytes(additionalLength);
                    break;
                case PktVersion.V3_1:
                    _reader.ReadByte();                        // sniffer id
                    SetBuild(_reader.ReadInt32());            // client build
                    _reader.ReadBytes(4);                    // client locale
                    _reader.ReadBytes(40);                    // session key
                    _reader.ReadUInt32();                    // start time
                    _reader.ReadUInt32();                    // start tick count
                    additionalLength = _reader.ReadInt32();
                    _reader.ReadBytes(additionalLength);
                    break;
            }
        }

        void SetBuild(int build)
        {
            if (ClientVersion.Version == ClientVersionBuild.Zero)
                ClientVersion.Version = (ClientVersionBuild)build;
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
                            time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
                        else
                        {
                            time = new DateTime();
                            // TODO: 3.1: time has to be computed: startTime.AddMilliseconds(tickcount - startTickCount).ToUnixTime()
                            _reader.ReadUInt32(); // session id
                        }

                        _reader.ReadInt32(); // tick count
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
                        time = Utilities.GetDateTimeFromUnixTime((int)_reader.ReadInt64());
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
