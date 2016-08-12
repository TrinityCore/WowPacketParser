using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GlyphProperties, HasIndexInData = false)]
    public class GlyphPropertiesEntry
    {
        public uint SpellID { get; set; }
        public ushort SpellIconID { get; set; }
        public byte Type { get; set; }
        public byte GlyphExclusiveCategoryID { get; set; }
    }
}