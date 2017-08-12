using System;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.SpellPower)]
    public class SpellPowerEntry
    {
        public uint ID { get; set; }
        public uint SpellID { get; set; }
        public uint PowerIndex { get; set; }
        public uint ManaCost { get; set; }
        public uint ManaCostPerLevel { get; set; }
        public uint ManaCostPerSecond { get; set; }
        public uint ManaCostAdditional { get; set; }
        public uint PowerDisplayID { get; set; }
        public uint UnitPowerBarID { get; set; }
        public Single ManaCostPercentage { get; set; }
        public Single ManaCostPercentagePerSecond { get; set; }
        public uint RequiredAura { get; set; }
        public Single HealthCostPercentage { get; set; }
    }
}