using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Exhaustion)]
    public class ExhaustionEntry
    {
        public string Name { get; set; }
        public string CombatLogText { get; set; }
        public int ID { get; set; }
        public int Xp { get; set; }
        public float Factor { get; set; }
        public float OutdoorHours { get; set; }
        public float InnHours { get; set; }
        public float Threshold { get; set; }
    }
}
