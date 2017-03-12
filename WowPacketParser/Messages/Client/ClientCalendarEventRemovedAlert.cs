namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCalendarEventRemovedAlert
    {
        public ulong EventID;
        public bool ClearPending;
        public uint Date;
    }
}
