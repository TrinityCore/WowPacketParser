namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarRaidLockoutRemoved
    {
        public ulong InstanceID;
        public int MapID;
        public uint DifficultyID;
    }
}
