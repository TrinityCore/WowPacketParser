using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.SpellCastingRequirements)]
    public class SpellCastingRequirementsEntry
    {
        public uint ID { get; set; }
        public uint FacingCasterFlags { get; set; }
        public uint MinFactionID { get; set; }
        public uint MinReputation { get; set; }
        public uint RequiredAreasID { get; set; }
        public uint RequiredAuraVision { get; set; }
        public uint RequiresSpellFocus { get; set; }
    }
}