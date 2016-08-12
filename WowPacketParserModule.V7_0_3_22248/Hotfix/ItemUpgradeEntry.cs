using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemUpgrade, HasIndexInData = false)]
    public class ItemUpgradeEntry
    {
        public uint CurrencyCost { get; set; }
        public ushort PrevItemUpgradeID { get; set; }
        public ushort CurrencyID { get; set; }
        public byte ItemUpgradePathID { get; set; }
        public byte ItemLevelBonus { get; set; }
    }
}