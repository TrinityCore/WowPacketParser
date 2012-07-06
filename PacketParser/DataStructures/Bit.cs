using System.Globalization;

namespace PacketParser.DataStructures
{
    public struct Bit
    {
        private readonly bool _value;

        public Bit(bool value) { _value = value; }
        public bool Value { get { return _value; } }
        public static implicit operator byte(Bit b) { return (byte)(b.Value ? 1 : 0); }
        public static implicit operator bool(Bit b) { return b.Value; }
        public static implicit operator Bit(bool b) { return new Bit(b); }
        public static implicit operator Bit(byte b) { return new Bit(b != 0); }
        public override string ToString() { return _value.ToString(CultureInfo.InvariantCulture); }
    }
}
