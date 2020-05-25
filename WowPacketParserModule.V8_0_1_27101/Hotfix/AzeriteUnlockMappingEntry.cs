using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
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
