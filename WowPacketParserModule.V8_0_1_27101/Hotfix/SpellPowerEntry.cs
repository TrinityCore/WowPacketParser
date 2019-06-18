using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellPower)]
    public class SpellPowerEntry
    {
        public int ID { get; set; }
        public byte OrderIndex { get; set; }
        public int ManaCost { get; set; }
        public int ManaCostPerLevel { get; set; }
        public int ManaPerSecond { get; set; }
        public uint PowerDisplayID { get; set; }
        public int AltPowerBarID { get; set; }
        public float PowerCostPct { get; set; }
        public float PowerCostMaxPct { get; set; }
        public float PowerPctPerSecond { get; set; }
        public sbyte PowerType { get; set; }
        public int RequiredAuraSpellID { get; set; }
        public uint OptionalCost { get; set; }
        public int SpellID { get; set; }
    }
}
