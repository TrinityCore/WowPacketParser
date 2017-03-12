namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarRaidLockoutAdded
    {
        public ulong InstanceID;
        public uint DifficultyID;
        public int TimeRemaining;
        public uint ServerTime;
        public int MapID;
    }
}
