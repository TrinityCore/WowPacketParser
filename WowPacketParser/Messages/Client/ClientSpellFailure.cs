namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellFailure
    {
        public ulong CasterUnit;
        public int SpellID;
        public byte Reason;
        public byte CastID;
    }
}
