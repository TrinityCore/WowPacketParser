namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQuestGiverQuestComplete
    {
        public bool UseQuestReward;
        public int SkillLineIDReward;
        public int MoneyReward;
        public int NumSkillUpsReward;
        public int XpReward;
        public int QuestId;
        public int TalentReward;
        public bool LaunchGossip;
    }
}
