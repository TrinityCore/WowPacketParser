namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct SpellProcsPerMinuteCalc
    {
        public int Type;
        public int Param;
        public float Coeff;
        public float Input;
        public float NewValue;
    }
}
