namespace WowPacketParser.Messages.UserClient.Calendar
{
    public unsafe struct RemoveEvent
    {
        public ulong ModeratorID;
        public ulong EventID;
        public uint Flags;
    }
}
