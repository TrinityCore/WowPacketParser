namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarEventInviteStatusAlert
    {
        public ulong EventID;
        public uint Flags;
        public uint Date;
        public byte Status;
    }
}
