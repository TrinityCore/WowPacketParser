using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.TaxiPath)]
    public class TaxiPathEntry
    {
        public int ID { get; set; }
        public ushort FromTaxiNode { get; set; }
        public ushort ToTaxiNode { get; set; }
        public uint Cost { get; set; }
    }
}
