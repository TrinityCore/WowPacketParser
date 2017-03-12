namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellInterruptLog
    {
        public ulong Victim;
        public ulong Caster;
        public int SpellID;
        public int InterruptedSpellID;
    }
}
