namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellOrDamageImmune
    {
        public ulong VictimGUID;
        public bool IsPeriodic;
        public uint SpellID;
        public ulong CasterGUID;
    }
}
