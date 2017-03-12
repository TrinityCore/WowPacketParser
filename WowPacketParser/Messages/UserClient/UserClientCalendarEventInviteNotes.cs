namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCalendarEventInviteNotes
    {
        public ulong EventID;
        public ulong Guid;
        public ulong InviteID;
        public ulong ModeratorID;
        public string Notes;
    }
}
