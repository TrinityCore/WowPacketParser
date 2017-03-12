namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarSendCalendarRaidLockoutInfo
    {
        public ulong InstanceID;
        public int MapID;
        public uint DifficultyID;
        public int ExpireTime;
    }
}
