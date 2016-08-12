using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellCastingRequirements, HasIndexInData = false)]
    public class SpellCastingRequirementsEntry
    {
        public uint SpellID { get; set; }
        public ushort MinFactionID { get; set; }
        public ushort RequiredAreasID { get; set; }
        public ushort RequiresSpellFocus { get; set; }
        public byte FacingCasterFlags { get; set; }
        public byte MinReputation { get; set; }
        public byte RequiredAuraVision { get; set; }
    }
}