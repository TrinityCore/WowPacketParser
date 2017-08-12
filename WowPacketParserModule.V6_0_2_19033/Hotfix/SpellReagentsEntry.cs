using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.SpellReagents)]
    public class SpellReagentsEntry
    {
        public uint ID { get; set; }
        [HotfixArray(8)]
        public uint[] Reagent { get; set; }
        [HotfixArray(8)]
        public uint[] ReagentCount { get; set; }
        public uint CurrencyID { get; set; }
        public uint CurrencyCount { get; set; }
    }
}