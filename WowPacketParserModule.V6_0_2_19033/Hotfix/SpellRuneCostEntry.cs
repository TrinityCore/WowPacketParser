using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.SpellRuneCost)]
    public class SpellRuneCostEntry
    {
        public uint ID { get; set; }
        public uint Blood { get; set; }
        public uint Unholy { get; set; }
        public uint Frost { get; set; }
        public uint Chromatic { get; set; }
        public uint RunicPower { get; set; }
    }
}