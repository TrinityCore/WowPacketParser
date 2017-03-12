namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellDelayed
    {
        public ulong Caster;
        public int ActualDelay;
    }
}
