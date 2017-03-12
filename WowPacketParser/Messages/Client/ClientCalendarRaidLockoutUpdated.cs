namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarRaidLockoutUpdated
    {
        public int MapID;
        public int OldTimeRemaining;
        public uint ServerTime;
        public uint DifficultyID;
        public int NewTimeRemaining;
    }
}
