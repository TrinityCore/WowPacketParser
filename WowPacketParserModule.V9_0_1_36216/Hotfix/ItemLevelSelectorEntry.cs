using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemLevelSelector, HasIndexInData = false)]
    public class ItemLevelSelectorEntry
    {
        public ushort MinItemLevel { get; set; }
        public ushort ItemLevelSelectorQualitySetID { get; set; }
        public ushort AzeriteUnlockMappingSet { get; set; }
    }
}
