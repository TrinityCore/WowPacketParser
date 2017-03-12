namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct ResearchHistory
    {
        public int ProjectID;
        public UnixTime FirstCompleted;
        public int CompletionCount;
    }
}
