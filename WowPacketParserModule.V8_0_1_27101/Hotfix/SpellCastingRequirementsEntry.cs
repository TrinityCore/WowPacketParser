using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellCastingRequirements, HasIndexInData = false)]
    public class SpellCastingRequirementsEntry
    {
        public int SpellID { get; set; }
        public byte FacingCasterFlags { get; set; }
        public ushort MinFactionID { get; set; }
        public sbyte MinReputation { get; set; }
        public ushort RequiredAreasID { get; set; }
        public byte RequiredAuraVision { get; set; }
        public ushort RequiresSpellFocus { get; set; }
    }
}
