using System;
using System.IO;
using System.Text;
using WowPacketParser.Enums.Battlenet;

namespace WowPacketParser.Misc
{
    public sealed class BattlenetPacket
    {
        private static BattlenetChannel lastChannel = BattlenetChannel.Auth;

        public BattlenetPacketHeader Header { get; set; }
        public int ProcessedBytes { get; set; }
        public Packet Stream;

        byte bytePart;
        int count;

        public BattlenetPacket(Packet packet)
        {
            Stream = packet;

            Header = new BattlenetPacketHeader();
            Header.Opcode = Read<byte>(6);

            if (Read<bool>(1))
                lastChannel = Header.Channel = (BattlenetChannel)Read<byte>(4);
            else
                Header.Channel = lastChannel;

            Header.Direction = packet.Direction;
        }

        public string GetHeader()
        {
            return string.Format("{0}: {1} (0x{2}) Channel: {3} Length: {4} Time: {5} Number: {6}",
                Stream.Direction, BattlenetOpcodeName.GetName(Header.Opcode, (byte)Header.Channel, Stream.Direction), Header.Opcode.ToString("X2"), Header.Channel,
                Stream.Length, Stream.Time.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                Stream.Number);
        }

        public T Read<T>(string name, int bits, params int[] values)
        {
            var value = Read<T>(bits);
            Stream.WriteLine("{0}{1}: {2}", Packet.GetIndexString(values), name, value);
            return value;
        }

        public string ReadString(string name, int length, params int[] values)
        {
            var value = ReadString(length);
            Stream.WriteLine("{0}{1}: {2}", Packet.GetIndexString(values), name, value);
            return value;
        }

        public string ReadFourCC(string name, params int[] values)
        {
            var value = ReadFourCC();
            Stream.WriteLine("{0}{1}: {2}", Packet.GetIndexString(values), name, value);
            return value;
        }

        public float ReadSingle(string name, params int[] values)
        {
            var value = ReadSingle();
            Stream.WriteLine("{0}{1}: {2}", Packet.GetIndexString(values), name, value);
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
            this.count = 0;

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
            var bitsToRead = 0;

            while (bits != 0)
            {
                if ((count % 8) == 0)
                {
                    bytePart = Read<byte>();

                    ProcessedBytes += 1;
                }

                var shiftedBits = count & 7;
                bitsToRead = 8 - shiftedBits;

                if (bitsToRead >= bits)
                    bitsToRead = bits;

                bits -= bitsToRead;
                unchecked
                {
                    value |= (ulong)(bytePart >> shiftedBits & (uint)((byte)(1 << bitsToRead) - 1)) << bits;
                }
                count += bitsToRead;
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }

        public string ReadFourCC()
        {
            var data = BitConverter.GetBytes(Read<uint>(32));

            Array.Reverse(data);

            return Encoding.UTF8.GetString(data).Trim(new char[] { '\0' });
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
