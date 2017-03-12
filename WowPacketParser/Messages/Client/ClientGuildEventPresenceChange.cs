namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildEventPresenceChange
    {
        public bool LoggedOn;
        public uint VirtualRealmAddress;
        public string Name;
        public ulong Guid;
        public bool Mobile;
    }
}
