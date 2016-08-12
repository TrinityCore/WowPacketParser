using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiPath)]
    public class TaxiPathEntry
    {
        public uint ID { get; set; }
        public uint From { get; set; }
        public uint To { get; set; }
        public uint Cost { get; set; }
    }
}