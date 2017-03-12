namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildChallengeCompleted
    {
        public int MaxCount;
        public int ChallengeType;
        public int GoldAwarded;
        public int XpAwarded;
        public int CurrentCount;
    }
}
