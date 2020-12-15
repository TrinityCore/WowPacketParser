using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemCurrencyCost, HasIndexInData = false)]
    public class ItemCurrencyCostEntry
    {
        public int ItemID { get; set; }
    }
}
