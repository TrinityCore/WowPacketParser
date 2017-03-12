namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCalendarEventInvite
    {
        public ulong ModeratorID;
        public bool IsSignUp;
        public bool Creating;
        public ulong EventID;
        public string Name;
    }
}
