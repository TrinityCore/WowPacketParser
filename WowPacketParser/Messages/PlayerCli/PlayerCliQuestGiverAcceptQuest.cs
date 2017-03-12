namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliQuestGiverAcceptQuest
    {
        public int QuestID;
        public ulong QuestGiverGUID;
        public bool StartCheat;
    }
}
