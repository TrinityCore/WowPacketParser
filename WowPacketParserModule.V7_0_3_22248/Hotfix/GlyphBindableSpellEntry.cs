using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GlyphBindableSpell, HasIndexInData = false)]
    public class GlyphBindableSpellEntry
    {
        public uint SpellID { get; set; }
        public ushort GlyphPropertiesID { get; set; }
    }
}
