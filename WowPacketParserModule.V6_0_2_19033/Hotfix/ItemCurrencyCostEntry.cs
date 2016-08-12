using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.ItemCurrencyCost)]
    public class ItemCurrencyCostEntry
    {
        public int ID { get; set; }
        public uint ItemId { get; set; }
    }
}