namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliSavedThrottleObjectState
    {
        public uint MaxTries;
        public uint PerMilliseconds;
        public uint TryCount;
        public uint LastResetTimeBeforeNow;
    }
}
