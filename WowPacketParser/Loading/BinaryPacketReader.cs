using System;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public class BinaryPacketReader : IPacketReader
    {
        private readonly BinaryReader _reader;
        private readonly SniffType _sniffType;

        public BinaryPacketReader(SniffType type, string fileName, Encoding encoding)
        {
            _sniffType = type;
            _reader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), encoding);
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
                opcode = _reader.ReadUInt16();
                length = _reader.ReadInt32();
                direction = (Direction)_reader.ReadByte();
                time = Utilities.GetDateTimeFromUnixTime((int)_reader.ReadInt64());
                data = _reader.ReadBytes(length);
            }
            else
            {
                opcode = _reader.ReadInt32();
                length = _reader.ReadInt32();
                time = Utilities.GetDateTimeFromUnixTime(_reader.ReadInt32());
                direction = (Direction)_reader.ReadChar();
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
