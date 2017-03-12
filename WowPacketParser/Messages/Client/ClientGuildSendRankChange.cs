namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildSendRankChange
    {
        public ulong Other;
        public bool Promote;
        public ulong Officer;
        public uint RankID;
    }
}
