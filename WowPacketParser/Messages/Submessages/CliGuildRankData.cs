namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliGuildRankData
    {
        public uint RankID;
        public uint RankOrder;
        public string RankName;
        public uint Flags;
        public uint WithdrawGoldLimit;
        public fixed uint TabFlags[8];
        public fixed uint TabWithdrawItemLimit[8];
    }
}
