using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteUnlockMapping, HasIndexInData = false)]
    public class AzeriteUnlockMappingEntry
    {
        public int ItemLevel { get; set; }
        public int ItemBonusListHead { get; set; }
        public int ItemBonusListShoulders { get; set; }
        public int ItemBonusListChest { get; set; }
        public uint AzeriteUnlockMappingSetID { get; set; }
    }
}
