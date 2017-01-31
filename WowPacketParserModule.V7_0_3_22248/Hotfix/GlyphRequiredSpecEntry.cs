using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GlyphRequiredSpec, HasIndexInData = false)]
    public class GlyphRequiredSpecEntry
    {
        public ushort GlyphPropertiesID { get; set; }
        public ushort ChrSpecializationID { get; set; }
    }
}
