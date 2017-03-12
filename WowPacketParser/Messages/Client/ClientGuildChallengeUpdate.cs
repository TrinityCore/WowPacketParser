namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildChallengeUpdate
    {
        public fixed int MaxCount[6];
        public fixed int Xp[6];
        public fixed int Gold[6];
        public fixed int CurrentCount[6];
        public fixed int MaxLevelGold[6];
    }
}
