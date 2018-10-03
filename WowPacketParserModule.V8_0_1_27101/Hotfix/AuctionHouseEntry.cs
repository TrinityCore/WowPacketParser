using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AuctionHouse, HasIndexInData = false)]
    public class AuctionHouseEntry
    {
        public string Name { get; set; }
        public ushort FactionID { get; set; }
        public byte DepositRate { get; set; }
        public byte ConsignmentRate { get; set; }
    }
}
