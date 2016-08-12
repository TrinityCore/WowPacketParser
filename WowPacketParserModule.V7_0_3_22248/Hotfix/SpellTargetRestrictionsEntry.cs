using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellTargetRestrictions, HasIndexInData = false)]
    public class SpellTargetRestrictionsEntry
    {
        public uint SpellID { get; set; }
        public float ConeAngle { get; set; }
        public float Width { get; set; }
        public uint Targets { get; set; }
        public ushort TargetCreatureType { get; set; }
        public byte DifficultyID { get; set; }
        public byte MaxAffectedTargets { get; set; }
        public uint MaxTargetLevel { get; set; }
    }
}