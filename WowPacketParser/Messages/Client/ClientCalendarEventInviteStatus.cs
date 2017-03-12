namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarEventInviteStatus
    {
        public uint Flags;
        public ulong EventID;
        public byte Status;
        public bool ClearPending;
        public uint ResponseTime;
        public uint Date;
        public ulong InviteGUID;
    }
}
