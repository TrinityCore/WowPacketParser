using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemLevelSelectorQualitySet, HasIndexInData = false)]
    public class ItemLevelSelectorQualitySetEntry
    {
        public short IlvlRare { get; set; }
        public short IlvlEpic { get; set; }
    }
}
