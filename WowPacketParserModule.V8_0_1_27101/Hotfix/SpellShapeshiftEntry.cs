using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellShapeshift, HasIndexInData = false)]
    public class SpellShapeshiftEntry
    {
        public int SpellID { get; set; }
        public sbyte StanceBarOrder { get; set; }
        [HotfixArray(2)]
        public int[] ShapeshiftExclude { get; set; }
        [HotfixArray(2)]
        public int[] ShapeshiftMask { get; set; }
    }
}
