namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientArchaeologySurveryCast
    {
        public int ResearchBranchID;
        public bool SuccessfulFind;
        public uint NumFindsCompleted;
        public uint TotalFinds;
    }
}
