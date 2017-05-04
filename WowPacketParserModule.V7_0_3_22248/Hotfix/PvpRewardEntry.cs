using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.PvpReward, HasIndexInData = false)]
    public class PvpRewardEntry
    {
        public uint HonorLevel { get; set; }
        public uint Prestige { get; set; }
        public uint RewardPackID { get; set; }
    }
}
