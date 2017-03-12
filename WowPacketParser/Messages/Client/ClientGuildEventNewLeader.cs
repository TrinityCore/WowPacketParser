namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildEventNewLeader
    {
        public uint NewLeaderVirtualRealmAddress;
        public uint OldLeaderVirtualRealmAddress;
        public string NewLeaderName;
        public ulong NewLeaderGUID;
        public bool SelfPromoted;
        public ulong OldLeaderGUID;
        public string OldLeaderName;
    }
}
