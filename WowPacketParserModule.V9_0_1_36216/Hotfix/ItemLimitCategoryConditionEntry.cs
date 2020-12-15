using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemLimitCategoryCondition, HasIndexInData = false)]
    public class ItemLimitCategoryConditionEntry
    {
        public sbyte AddQuantity { get; set; }
        public uint PlayerConditionID { get; set; }
        public uint ParentItemLimitCategoryID { get; set; }
    }
}
