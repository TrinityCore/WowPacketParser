using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GlyphBindableSpell, HasIndexInData = false)]
    public class GlyphBindableSpellEntry
    {
        public int SpellID { get; set; }
        public short GlyphPropertiesID { get; set; }
    }
}
