using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BankBagSlotPrices, HasIndexInData = false)]
    public class BankBagSlotPricesEntry
    {
        public uint Cost { get; set; }
    }
}
