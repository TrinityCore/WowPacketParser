namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAddLossOfControl
    {
        public uint DurationRemaining;
        public ulong Caster;
        public uint Duration;
        public uint LockoutSchoolMask;
        public int SpellID;
        public int Type;
        public int Mechanic;
    }
}
