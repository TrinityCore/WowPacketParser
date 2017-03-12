namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPendingRaidLock
    {
        public uint CompletedMask;
        public bool WarningOnly;
        public int TimeUntilLock;
        public bool Extending;
    }
}
