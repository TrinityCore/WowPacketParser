namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCalendarCopyEvent
    {
        public ulong ModeratorID;
        public ulong EventID;
        public uint Date;
    }
}
