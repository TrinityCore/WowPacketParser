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

            Header = new BattlenetPacketHeader {Opcode = Read<byte>(6)};

            if (Read<bool>(1))
                Header.Channel = (BattlenetChannel)Read<byte>(4);

            Header.Direction = packet.Direction;
        }

        public string GetHeader()
        {
            return string.Format("{0}: {1} (0x{2}) Channel: {3} Length: {4} Time: {5} Number: {6}",
                Stream.Direction, BattlenetOpcodeName.GetName(Header.Opcode, (byte)Header.Channel, Stream.Direction), Header.Opcode.ToString("X2"), Header.Channel,
                Stream.Length, Stream.Time.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                Stream.Number);
        }

        public T Read<T>(string name, int bits, params object[] indexes)
        {
            var value = Read<T>(bits);
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public string ReadString(string name, int length, params object[] indexes)
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

        public byte[] ReadBytes(string name, int length, params object[] indexes)
        {
            var value = ReadBytes(length);
            Stream.AddValue(name, Utilities.ByteArrayToHexString(value), indexes);
            return value;
        }

        public T Read<T>()
        {
            var type = typeof(T).IsEnum ? typeof(T).GetEnumUnderlyingType() : typeof(T);
            object value;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    value = Stream.ReadBoolean();
                    break;
                case TypeCode.SByte:
                    value = Stream.ReadSByte();
                    break;
                case TypeCode.Byte:
                    value = Stream.ReadByte();
                    break;
                case TypeCode.Char:
                    value = Stream.ReadChar();
                    break;
                case TypeCode.Int16:
                    value = Stream.ReadInt16();
                    break;
                case TypeCode.UInt16:
                    value = Stream.ReadUInt16();
                    break;
                case TypeCode.Int32:
                    value = Stream.ReadInt32();
                    break;
                case TypeCode.UInt32:
                    value = Stream.ReadUInt32();
                    break;
                case TypeCode.Int64:
                    value = Stream.ReadInt64();
                    break;
                case TypeCode.UInt64:
                    value = Stream.ReadUInt64();
                    break;
                case TypeCode.Single:
                    value = Stream.ReadSingle();
                    break;
                case TypeCode.Double:
                    value = Stream.ReadDouble();
                    break;
                default:
                    throw new InvalidCastException("");
            }

            return (T)value;
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

        public T Read<T>(int bits)
        {
            ulong value = 0;

            while (bits != 0)
            {
                if ((_count % 8) == 0)
                {
                    _bytePart = Read<byte>();

                    ProcessedBytes += 1;
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

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public string ReadFourCC()
        {
            var data = BitConverter.GetBytes(Read<uint>(32));

            Array.Reverse(data);

            return Encoding.UTF8.GetString(data).Trim('\0');
        }

        public float ReadSingle()
        {
            var intVal = Read<int>(32);
            using (var mem = new MemoryStream(4))
            using (var wrt = new BinaryWriter(mem))
            using (var rdr = new BinaryReader(mem))
            {
                wrt.Write(intVal);
                mem.Position = 0;
                return rdr.ReadSingle();
            }
        }
    }
}
