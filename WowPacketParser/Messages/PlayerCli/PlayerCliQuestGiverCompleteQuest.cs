namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliQuestGiverCompleteQuest
    {
        public bool FromScript;
        public int QuestID;
        public ulong QuestGiverGUID;
    }
}
