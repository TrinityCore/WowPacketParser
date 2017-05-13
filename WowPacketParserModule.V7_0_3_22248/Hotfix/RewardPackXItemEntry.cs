using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.RewardPackXItem, HasIndexInData = false)]
    public class RewardPackXItemEntry
    {
        public uint ItemID { get; set; }
        public uint RewardPackID { get; set; }
        public uint Amount { get; set; }
    }
}
