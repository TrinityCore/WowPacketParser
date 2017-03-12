namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarEventInviteRemovedAlert
    {
        public ulong EventID;
        public uint Date;
        public uint Flags;
        public byte Status;
    }
}
