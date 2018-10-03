using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemLimitCategoryCondition, HasIndexInData = false)]
    public class ItemLimitCategoryConditionEntry
    {
        public sbyte AddQuantity { get; set; }
        public uint PlayerConditionID { get; set; }
        public int ParentItemLimitCategoryID { get; set; }
    }
}
