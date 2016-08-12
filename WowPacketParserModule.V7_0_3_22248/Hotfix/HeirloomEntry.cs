using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Heirloom)]
    public class HeirloomEntry
    {
        public uint ItemID { get; set; }
        public string SourceText { get; set; }
        [HotfixArray(2)]
        public uint[] OldItem { get; set; }
        public uint NextDifficultyItemID { get; set; }
        [HotfixArray(2)]
        public uint[] UpgradeItemID { get; set; }
        [HotfixArray(2)]
        public ushort[] ItemBonusListID { get; set; }
        public byte Flags { get; set; }
        public byte Source { get; set; }
        public uint ID { get; set; }
    }
}