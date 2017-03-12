using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct LfgPlayerDungeonInfo
    {
        public uint Slot;
        public bool FirstReward;
        public int CompletionQuantity;
        public int CompletionLimit;
        public int CompletionCurrencyID;
        public int SpecificQuantity;
        public int SpecificLimit;
        public int OverallQuantity;
        public int OverallLimit;
        public int PurseWeeklyQuantity;
        public int PurseWeeklyLimit;
        public int PurseQuantity;
        public int PurseLimit;
        public int Quantity;
        public uint CompletedMask;
        public bool ShortageEligible;
        public List<LfgPlayerQuestReward> ShortageReward;
        public LfgPlayerQuestReward Rewards;
    }
}
