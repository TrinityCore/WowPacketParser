namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCalendarEventStatus
    {
        public ulong ModeratorID;
        public ulong EventID;
        public ulong InviteID;
        public ulong Guid;
        public byte Status;
    }
}
