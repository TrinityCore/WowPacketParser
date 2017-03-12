namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellFailedOther
    {
        public ulong CasterUnit;
        public int SpellID;
        public byte Reason;
        public byte CastID;
    }
}
