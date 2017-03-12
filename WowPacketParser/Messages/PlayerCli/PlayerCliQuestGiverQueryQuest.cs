namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliQuestGiverQueryQuest
    {
        public int QuestID;
        public ulong QuestGiverGUID;
        public bool RespondToGiver;
    }
}
