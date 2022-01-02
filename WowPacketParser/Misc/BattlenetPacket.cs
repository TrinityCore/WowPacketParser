using System;
using WowPacketParser.Enums.Battlenet;

namespace WowPacketParser.Misc
{
    public sealed class BattlenetPacket
    {
        public BattlenetPacketHeader Header { get; set; }
        public int ProcessedBytes => BitStream.ProcessedBytes;
        public readonly Packet Stream;
        public readonly BattlenetBitStream BitStream;

        public BattlenetPacket(Packet packet)
        {
            BitStream = new BattlenetBitStream(packet);
            Stream = packet;

            ushort opcode = BitStream.Read<byte>(0, 6);

            BattlenetChannel channel = BattlenetChannel.Authentication;
            if (ReadBoolean())
                channel = (BattlenetChannel)BitStream.Read<byte>(0, 4);

            Header = new BattlenetPacketHeader(opcode, channel, packet.Direction);
        }

        public string GetHeader()
        {
            // ReSharper disable once UseStringInterpolation
            return string.Format("{0}: {1} (0x{2}) Channel: {3} Length: {4} Time: {5} Number: {6}",
                Stream.Direction, CommandNames.Get(Header.Opcode, Header.Channel, Stream.Direction), Header.Opcode.ToString("X2"), Header.Channel,
                Stream.Length, Stream.Time.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                Stream.Number);
        }

        public T Read<T>(string name, long minValue, int bits, params object[] indexes)
        {
            var value = BitStream.Read<T>(minValue, bits);
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public bool ReadBoolean(string name, params object[] indexes)
        {
            var value = BitStream.ReadBoolean();
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public string ReadString(string name, long minLength, int lengthBits, params object[] indexes)
        {
            return ReadFixedLengthString(name, BitStream.Read<int>(minLength, lengthBits), indexes);
        }

        public string ReadFixedLengthString(string name, int length, params object[] indexes)
        {
            var value = BitStream.ReadString(length);
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public string ReadFourCC(string name, params object[] indexes)
        {
            var value = BitStream.ReadFourCC();
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public float ReadSingle(string name, params object[] indexes)
        {
            var value = BitStream.ReadSingle();
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public double ReadDouble(string name, params object[] indexes)
        {
            var value = BitStream.ReadDouble();
            Stream.AddValue(name, value, indexes);
            return value;
        }

        public byte[] ReadByteArray(string name, long minLength, int lengthBits, params object[] indexes)
        {
            return ReadBytes(name, BitStream.Read<int>(minLength, lengthBits), indexes);
        }

        public byte[] ReadBytes(string name, int length, params object[] indexes)
        {
            var value = BitStream.ReadBytes(length);
            Stream.AddValue(name, Convert.ToHexString(value), indexes);
            return value;
        }

        public byte[] ReadBytes(int count)
        {
            return BitStream.ReadBytes(count);
        }

        private string ReadString(int count)
        {
            return BitStream.ReadString(count);
        }

        public void ReadSkip(int bits)
        {
            BitStream.ReadSkip(bits);
        }

        public T Read<T>(long minValue, int bits)
        {
            return BitStream.Read<T>(minValue, bits);
        }

        public string ReadFourCC()
        {
            return BitStream.ReadFourCC();
        }

        public float ReadSingle()
        {
            return BitStream.ReadSingle();
        }

        public double ReadDouble()
        {
            return BitStream.ReadDouble();
        }

        public bool ReadBoolean()
        {
            return BitStream.Read<bool>(0, 1);
        }
    }
}
