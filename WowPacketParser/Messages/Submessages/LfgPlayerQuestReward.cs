using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LfgPlayerQuestReward
    {
        public uint Mask;
        public int RewardMoney;
        public int RewardXP;
        public List<LfgPlayerQuestRewardItem> Item;
        public List<LfgPlayerQuestRewardCurrency> Currency;
        public List<LfgPlayerQuestRewardCurrency> BonusCurrency;
    }
}
