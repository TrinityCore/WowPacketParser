namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCalendarEventRSVP
    {
        public ulong InviteID;
        public ulong EventID;
        public byte Status;
    }
}
