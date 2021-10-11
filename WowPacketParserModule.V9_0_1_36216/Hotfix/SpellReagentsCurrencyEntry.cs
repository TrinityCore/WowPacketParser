using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellReagentsCurrency, HasIndexInData = false)]
    public class SpellReagentsCurrencyEntry
    {
        public int SpellID { get; set; }
        public ushort CurrencyTypesID { get; set; }
        public ushort CurrencyCount { get; set; }
    }
}
