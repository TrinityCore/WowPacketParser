using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiPath)]
    public class TaxiPathEntry
    {
        public uint ID { get; set; }
        public ushort FromTaxiNode { get; set; }
        public ushort ToTaxiNode { get; set; }
        public uint Cost { get; set; }
    }
}
