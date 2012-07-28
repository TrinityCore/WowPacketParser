namespace WowPacketParser.Misc
{
    public class Bit
    {
        Bit(bool value) { Value = value; }
        Bit(byte value) { Value = value != 0; }
        public bool Value { get; set; }
        public static implicit operator byte(Bit b) { return (byte)(b.Value == true ? 1 : 0); }
        public static implicit operator bool(Bit b) { return b.Value; }
        public static implicit operator Bit(bool b) { return new Bit(b); }
        public static implicit operator Bit(byte b) { return new Bit(b); }
    }
}
