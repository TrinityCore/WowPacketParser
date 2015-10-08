using System;
using System.IO;
using System.Text;
using WowPacketParser.Enums.Battlenet;

namespace WowPacketParser.Misc
{
    public sealed class BattlenetPacket
    {
        public BattlenetPacketHeader Header { get; set; }
        public int ProcessedBytes { get; set; }
        public readonly Packet Stream;

        private byte _bytePart;
        private int _count;

        public BattlenetPacket(Packet packet)
        {
            Stream = packet;

            Header = new BattlenetPacketHeader {Opcode = Read<byte>(0, 6)};

            if (ReadBoolean())
                Header.Channel = (BattlenetChannel)Read<byte>(0, 4);

            Header.Direction = packet.Direction;
        }

        public string GetHeader()
        {
            return string.Format("{0}: {1} (0x{2}) Channel: {3} Length: {4} Time: {5} Number: {6}",
                Stream.Direction, CommandNames.Get(Header.Opcode, Header.Channel, Stream.Direction), Header.Opcode.ToString("X2"), Header.Channel,
                Stream.Length, Stream.Time.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                Stream.Number);
        }

        public T Read<T>(string name, long minValue, int bits, params object[] indexes)
        {
            var value = Read<T>(minValue, bits);
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public bool ReadBoolean(string name, params object[] indexes)
        {
            var value = ReadBoolean();
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public string ReadString(string name, long minLength, int lengthBits, params object[] indexes)
        {
            return ReadFixedLengthString(name, Read<int>(minLength, lengthBits), indexes);
        }

        public string ReadFixedLengthString(string name, int length, params object[] indexes)
        {
            var value = ReadString(length);
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public string ReadFourCC(string name, params object[] indexes)
        {
            var value = ReadFourCC();
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public float ReadSingle(string name, params object[] indexes)
        {
            var value = ReadSingle();
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public byte[] ReadByteArray(string name, long minLength, int lengthBits, params object[] indexes)
        {
            return ReadBytes(name, Read<int>(minLength, lengthBits), indexes);
        }

        public byte[] ReadBytes(string name, int length, params object[] indexes)
        {
            var value = ReadBytes(length);
            Stream.AddValue(name, Utilities.ByteArrayToHexString(value), indexes);
            return value;
        }

        public byte[] ReadBytes(int count)
        {
            _count = 0;

            ProcessedBytes += count;

            return Stream.ReadBytes(count);
        }

        private string ReadString(int count)
        {
            return Encoding.UTF8.GetString(ReadBytes(count));
        }

        public void ReadSkip(int bits)
        {
            while (bits != 0)
            {
                if ((_count % 8) == 0)
                {
                    _bytePart = Stream.ReadByte();
                    ++ProcessedBytes;
                }

                var shiftedBits = _count & 7;
                int bitsToRead = 8 - shiftedBits;

                if (bitsToRead >= bits)
                    bitsToRead = bits;

                bits -= bitsToRead;
                _count += bitsToRead;
            }
        }

        public T Read<T>(long minValue, int bits)
        {
            ulong value = 0;

            while (bits != 0)
            {
                if ((_count % 8) == 0)
                {
                    _bytePart = Stream.ReadByte();
                    ++ProcessedBytes;
                }

                var shiftedBits = _count & 7;
                int bitsToRead = 8 - shiftedBits;

                if (bitsToRead >= bits)
                    bitsToRead = bits;

                bits -= bitsToRead;
                unchecked
                {
                    value |= (ulong)(_bytePart >> shiftedBits & (uint)((byte)(1 << bitsToRead) - 1)) << bits;
                }
                _count += bitsToRead;
            }

            unchecked
            {
                value = value + (ulong)minValue;
            }
            return (T)Convert.ChangeType(value, typeof(T).IsEnum ? typeof(T).GetEnumUnderlyingType() : typeof(T));
        }

        public string ReadFourCC()
        {
            var data = BitConverter.GetBytes(Read<uint>(0, 32));

            Array.Reverse(data);

            return Encoding.UTF8.GetString(data).Trim('\0');
        }

        public float ReadSingle()
        {
            var intVal = Read<int>(0, 32);
            using (var mem = new MemoryStream(4))
            using (var wrt = new BinaryWriter(mem))
            using (var rdr = new BinaryReader(mem))
            {
                wrt.Write(intVal);
                mem.Position = 0;
                return rdr.ReadSingle();
            }
        }

        public bool ReadBoolean()
        {
            return Read<bool>(0, 1);
        }
    }
}
