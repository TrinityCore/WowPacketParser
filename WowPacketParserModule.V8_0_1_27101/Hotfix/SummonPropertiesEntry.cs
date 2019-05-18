using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SummonProperties, HasIndexInData = false)]
    public class SummonPropertiesEntry
    {
        public int Control { get; set; }
        public int Faction { get; set; }
        public int Title { get; set; }
        public int Slot { get; set; }
        public int Flags { get; set; }
    }
}
