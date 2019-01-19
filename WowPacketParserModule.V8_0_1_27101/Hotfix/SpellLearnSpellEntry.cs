using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellLearnSpell, HasIndexInData = false)]
    public class SpellLearnSpellEntry
    {
        public int SpellID { get; set; }
        public int LearnSpellID { get; set; }
        public int OverridesSpellID { get; set; }
    }
}
