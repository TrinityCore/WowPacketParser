namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientXPGainAborted
    {
        public ulong Victim;
        public int XpToAdd;
        public int XpGainReason;
        public int XpAbortReason;
    }
}
