using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemLevelSelectorQualitySet, HasIndexInData = false)]
    public class ItemLevelSelectorQualitySetEntry
    {
        public short IlvlRare { get; set; }
        public short IlvlEpic { get; set; }
    }
}
