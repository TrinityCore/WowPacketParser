namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliQuestGiverChooseReward
    {
        public ulong QuestGiverGUID;
        public int QuestID;
        public int ItemChoiceID;
    }
}
