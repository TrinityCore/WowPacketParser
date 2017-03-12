namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarSendCalendarInviteInfo
    {
        public ulong EventID;
        public ulong InviteID;
        public ulong InviterGUID;
        public byte Status;
        public byte Moderator;
        public byte InviteType;
    }
}
