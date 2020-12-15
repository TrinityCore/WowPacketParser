using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.RewardPackXItem, HasIndexInData = false)]
    public class RewardPackXItemEntry
    {
        public int ItemID { get; set; }
        public int ItemQuantity { get; set; }
        public uint RewardPackID { get; set; }
    }
}
