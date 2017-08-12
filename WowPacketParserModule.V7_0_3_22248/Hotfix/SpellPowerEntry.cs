using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellPower)]
    public class SpellPowerEntry
    {
        public uint SpellID { get; set; }
        public uint ManaCost { get; set; }
        public float ManaCostPercentage { get; set; }
        public float ManaCostPercentagePerSecond { get; set; }
        public uint RequiredAura { get; set; }
        public float HealthCostPercentage { get; set; }
        public byte PowerIndex { get; set; }
        public byte PowerType { get; set; }
        public uint ID { get; set; }
        public uint ManaCostPerLevel { get; set; }
        public uint ManaCostPerSecond { get; set; }
        public uint ManaCostAdditional { get; set; }
        public uint PowerDisplayID { get; set; }
        public uint UnitPowerBarID { get; set; }
    }
}