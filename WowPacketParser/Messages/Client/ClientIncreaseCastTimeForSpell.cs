namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientIncreaseCastTimeForSpell
    {
        public ulong Caster;
        public uint CastTime;
        public int SpellID;
    }
}
