using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.Heirloom)]
    public class HeirloomEntry
    {
        public string SourceText { get; set; }
        public uint ID { get; set; }
        public int ItemID { get; set; }
        public int LegacyUpgradedItemID { get; set; }
        public int StaticUpgradedItemID { get; set; }
        public sbyte SourceTypeEnum { get; set; }
        public byte Flags { get; set; }
        public int LegacyItemID { get; set; }
        [HotfixArray(4)]
        public int[] UpgradeItemID { get; set; }
        [HotfixArray(4)]
        public ushort[] UpgradeItemBonusListID { get; set; }
    }
}
