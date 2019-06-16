using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_1_5_29683.Hotfix
{
    [HotfixStructure(DB2Hash.Heirloom, ClientVersionBuild.V8_1_5_29683)]
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
