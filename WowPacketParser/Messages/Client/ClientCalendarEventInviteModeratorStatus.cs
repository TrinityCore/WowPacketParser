namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarEventInviteModeratorStatus
    {
        public byte Status;
        public ulong InviteGUID;
        public ulong EventID;
        public bool ClearPending;
    }
}
