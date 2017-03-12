namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCalendarEventModeratorStatus
    {
        public ulong InviteID;
        public ulong EventID;
        public ulong Guid;
        public ulong ModeratorID;
        public byte Status;
    }
}
