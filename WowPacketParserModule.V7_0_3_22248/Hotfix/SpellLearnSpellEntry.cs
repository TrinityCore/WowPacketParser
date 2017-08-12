using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellLearnSpell, HasIndexInData = false)]
    public class SpellLearnSpellEntry
    {
        public uint LearnSpellID { get; set; }
        public uint SpellID { get; set; }
        public uint OverridesSpellID { get; set; }
    }
}