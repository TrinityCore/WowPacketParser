using System;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public class BinaryPacketReader : IPacketReader
    {
        private BinaryReader reader = null;
        private SniffType sniffType;

        public BinaryPacketReader(SniffType type, string fileName, Encoding encoding)
        {
            sniffType = type;
            reader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), encoding);
        }

        public bool CanRead()
        {
            return reader.BaseStream.Position != reader.BaseStream.Length;
        }

        public Packet Read(int number)
        {
            Opcode opcode = 0;
            var length = 0;
            DateTime time = DateTime.Now;
            Direction direction = 0;
            byte[] data = { };

            if (sniffType == SniffType.Pkt)
            {
                opcode = (Opcode)reader.ReadUInt16();
                length = reader.ReadInt32();
                direction = (Direction)reader.ReadByte();
                time = Utilities.GetDateTimeFromUnixTime((int)reader.ReadInt64());
                data = reader.ReadBytes(length);
            }
            else
            {
                opcode = (Opcode)reader.ReadInt32();
                length = reader.ReadInt32();
                time = Utilities.GetDateTimeFromUnixTime(reader.ReadInt32());
                direction = (Direction)reader.ReadChar();
                data = reader.ReadBytes(length);
            }

            return new Packet(data, opcode, time, direction, number);
        }

        public void Close()
        {
            if (reader != null)
                reader.Close();
        }
    }
}
