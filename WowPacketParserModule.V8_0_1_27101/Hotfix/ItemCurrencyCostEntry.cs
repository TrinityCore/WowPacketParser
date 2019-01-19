using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemCurrencyCost, HasIndexInData = false)]
    public class ItemCurrencyCostEntry
    {
        public int ItemID { get; set; }
    }
}
