namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildEventPlayerJoined
    {
        public ulong Guid;
        public string Name;
        public uint VirtualRealmAddress;
    }
}
