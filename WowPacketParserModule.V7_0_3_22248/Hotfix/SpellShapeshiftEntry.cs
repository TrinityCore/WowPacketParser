using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellShapeshift, HasIndexInData = false)]
    public class SpellShapeshiftEntry
    {
        public uint SpellID { get; set; }
        [HotfixArray(2)]
        public uint[] ShapeshiftExclude { get; set; }
        [HotfixArray(2)]
        public uint[] ShapeshiftMask { get; set; }
        public byte StanceBarOrder { get; set; }
    }
}