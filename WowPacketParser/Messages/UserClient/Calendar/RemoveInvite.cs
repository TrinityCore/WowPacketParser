namespace WowPacketParser.Messages.UserClient.Calendar
{
    public unsafe struct RemoveInvite
    {
        public ulong ModeratorID;
        public ulong Guid;
        public ulong EventID;
        public ulong InviteID;
    }
}
