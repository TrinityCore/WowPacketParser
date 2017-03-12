namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQuestUpdateAddCreditSimple
    {
        public int QuestID;
        public int ObjectID;
        public byte ObjectiveType;
    }
}
