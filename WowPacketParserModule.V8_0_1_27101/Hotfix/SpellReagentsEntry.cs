using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellReagents, HasIndexInData = false)]
    public class SpellReagentsEntry
    {
        public int SpellID { get; set; }
        [HotfixArray(8)]
        public int[] Reagent { get; set; }
        [HotfixArray(8)]
        public ushort[] ReagentCount { get; set; }
    }
}
