namespace WowPacketParser.Messages.UserClient.CalendarEvent
{
    public unsafe struct Status
    {
        public ulong ModeratorID;
        public ulong EventID;
        public ulong InviteID;
        public ulong Guid;
        public byte StatusValue;
    }
}
