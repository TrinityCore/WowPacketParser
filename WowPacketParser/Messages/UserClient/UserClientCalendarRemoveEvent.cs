namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCalendarRemoveEvent
    {
        public ulong ModeratorID;
        public ulong EventID;
        public uint Flags;
    }
}
