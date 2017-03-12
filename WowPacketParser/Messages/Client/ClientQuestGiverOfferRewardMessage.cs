using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQuestGiverOfferRewardMessage
    {
        public int PortraitTurnIn;
        public int PortraitGiver;
        public string PortraitGiverText;
        public string QuestTitle;
        public string PortraitTurnInText;
        public string PortraitGiverName;
        public string RewardText;
        public string PortraitTurnInName;
        public QuestGiverOfferReward QuestData;
        public int QuestPackageID;
    }
}
