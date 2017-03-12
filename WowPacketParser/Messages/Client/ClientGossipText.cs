namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGossipText
    {
        public int QuestID;
        public int QuestType;
        public int QuestLevel;
        public bool Repeatable;
        public string QuestTitle;
        public fixed int QuestFlags[2];
    }
}
