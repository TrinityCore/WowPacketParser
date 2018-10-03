using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemLevelSelector, HasIndexInData = false)]
    public class ItemLevelSelectorEntry
    {
        public ushort MinItemLevel { get; set; }
        public ushort ItemLevelSelectorQualitySetID { get; set; }
    }
}
