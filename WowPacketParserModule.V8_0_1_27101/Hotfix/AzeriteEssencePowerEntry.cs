using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteEssencePower, HasIndexInData = false)]
    public class AzeriteEssencePowerEntry
    {
        public string SourceAlliance { get; set; }
        public string SourceHorde { get; set; }
        public int AzeriteEssenceID { get; set; }
        public byte Tier { get; set; }
        public int MajorPowerDescription { get; set; }
        public int MinorPowerDescription { get; set; }
        public int MajorPowerActual { get; set; }
        public int MinorPowerActual { get; set; }
    }
}
