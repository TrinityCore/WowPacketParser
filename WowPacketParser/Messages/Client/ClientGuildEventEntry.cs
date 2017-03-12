namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildEventEntry
    {
        public ulong PlayerGUID;
        public ulong OtherGUID;
        public uint TransactionDate;
        public byte TransactionType;
        public byte RankID;
    }
}
