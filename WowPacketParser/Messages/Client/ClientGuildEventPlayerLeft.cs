namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildEventPlayerLeft
    {
        public string LeaverName;
        public uint LeaverVirtualRealmAddress;
        public bool Removed;
        public ulong LeaverGUID;
        public uint RemoverVirtualRealmAddress;
        public string RemoverName;
        public ulong RemoverGUID;
    }
}
