using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.GlyphProperties, HasIndexInData = false)]
    public class GlyphPropertiesEntry
    {
        public uint SpellID { get; set; }
        public byte GlyphType { get; set; }
        public byte GlyphExclusiveCategoryID { get; set; }
        public int SpellIconFileDataID { get; set; }
    }
}
