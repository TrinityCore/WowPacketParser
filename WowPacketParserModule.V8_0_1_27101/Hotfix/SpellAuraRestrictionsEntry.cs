using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellAuraRestrictions, HasIndexInData = false)]
    public class SpellAuraRestrictionsEntry
    {
        public byte DifficultyID { get; set; }
        public byte CasterAuraState { get; set; }
        public byte TargetAuraState { get; set; }
        public byte ExcludeCasterAuraState { get; set; }
        public byte ExcludeTargetAuraState { get; set; }
        public int CasterAuraSpell { get; set; }
        public int TargetAuraSpell { get; set; }
        public int ExcludeCasterAuraSpell { get; set; }
        public int ExcludeTargetAuraSpell { get; set; }
        public int SpellID { get; set; }
    }
}
