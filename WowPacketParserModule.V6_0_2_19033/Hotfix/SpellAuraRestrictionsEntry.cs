using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.SpellAuraRestrictions)]
    public class SpellAuraRestrictionsEntry
    {
        public uint ID { get; set; }
        public uint CasterAuraState { get; set; }
        public uint TargetAuraState { get; set; }
        public uint ExcludeCasterAuraState { get; set; }
        public uint ExcludeTargetAuraState { get; set; }
        public uint CasterAuraSpell { get; set; }
        public uint TargetAuraSpell { get; set; }
        public uint ExcludeCasterAuraSpell { get; set; }
        public uint ExcludeTargetAuraSpell { get; set; }
    }
}