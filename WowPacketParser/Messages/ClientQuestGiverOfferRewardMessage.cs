using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
