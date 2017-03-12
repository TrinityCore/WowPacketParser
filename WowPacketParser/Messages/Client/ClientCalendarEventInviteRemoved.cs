namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarEventInviteRemoved
    {
        public bool ClearPending;
        public uint Flags;
        public ulong InviteGUID;
        public ulong EventID;
    }
}
