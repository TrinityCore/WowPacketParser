namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQuestConfirmAccept
    {
        public string QuestTitle;
        public ulong InitiatedBy;
        public int QuestID;
    }
}
