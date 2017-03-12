namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQuestUpdateAddCredit
    {
        public ulong VictimGUID;
        public int ObjectID;
        public int QuestID;
        public ushort Count;
        public ushort Required;
        public byte ObjectiveType;
    }
}
