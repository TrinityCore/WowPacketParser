using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiPath)]
    public class TaxiPathEntry
    {
        public ushort From { get; set; }
        public ushort To { get; set; }
        public uint ID { get; set; }
        public uint Cost { get; set; }
    }
}