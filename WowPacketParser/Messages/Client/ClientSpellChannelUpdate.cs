namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellChannelUpdate
    {
        public ulong CasterGUID;
        public int TimeRemaining;
    }
}
