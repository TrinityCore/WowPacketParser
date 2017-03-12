namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCalendarRemoveInvite
    {
        public ulong ModeratorID;
        public ulong Guid;
        public ulong EventID;
        public ulong InviteID;
    }
}
