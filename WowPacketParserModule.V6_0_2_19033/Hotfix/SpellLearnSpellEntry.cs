using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.SpellLearnSpell)]
    public class SpellLearnSpellEntry
    {
        public uint ID { get; set; }
        public uint LearnSpellID { get; set; }
        public uint SpellID { get; set; }
        public uint OverridesSpellID { get; set; }
    }
}