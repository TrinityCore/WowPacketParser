using System;
using System.IO;
using System.Text;

namespace WowPacketParser.Misc
{
    public sealed class BattlenetBitStream
    {
        public int ProcessedBytes { get; set; }
        public readonly BinaryReader Stream;

        private byte _bytePart;
        private int _count;

        public BattlenetBitStream(BinaryReader reader)
        {
            Stream = reader;
        }

        public BattlenetBitStream(byte[] data)
        {
            Stream = new BinaryReader(new MemoryStream(data));
        }

        public byte[] ReadBytes(int count)
        {
            _count = 0;

            ProcessedBytes += count;

            return Stream.ReadBytes(count);
        }

        public string ReadString(int count)
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
            var intVal = Read<uint>(0, 32);
            using (var mem = new MemoryStream(4))
            using (var wrt = new BinaryWriter(mem))
            using (var rdr = new BinaryReader(mem))
            {
                wrt.Write(intVal);
                mem.Position = 0;
                return rdr.ReadSingle();
            }
        }

        public double ReadDouble()
        {
            var intVal = Read<ulong>(0, 64);
            using (var mem = new MemoryStream(8))
            using (var wrt = new BinaryWriter(mem))
            using (var rdr = new BinaryReader(mem))
            {
                wrt.Write(intVal);
                mem.Position = 0;
                return rdr.ReadDouble();
            }
        }

        public bool ReadBoolean()
        {
            return Read<bool>(0, 1);
        }
    }
}
