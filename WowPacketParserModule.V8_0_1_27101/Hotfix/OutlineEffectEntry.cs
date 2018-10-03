using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.OutlineEffect, HasIndexInData = false)]
    public class OutlineEffectEntry
    {
        public uint PassiveHighlightColorID { get; set; }
        public uint HighlightColorID { get; set; }
        public int Priority { get; set; }
        public int Flags { get; set; }
        public float Range { get; set; }
        [HotfixArray(2)]
        public uint[] UnitConditionID { get; set; }
    }
}
