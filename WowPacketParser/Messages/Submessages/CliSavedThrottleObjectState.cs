namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliSavedThrottleObjectState
    {
        public uint MaxTries;
        public uint PerMilliseconds;
        public uint TryCount;
        public uint LastResetTimeBeforeNow;
    }
}
