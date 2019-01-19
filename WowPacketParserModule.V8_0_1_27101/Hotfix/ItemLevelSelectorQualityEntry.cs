using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemLevelSelectorQuality, HasIndexInData = false)]
    public class ItemLevelSelectorQualityEntry
    {
        public int QualityItemBonusListID { get; set; }
        public sbyte Quality { get; set; }
        public short ParentILSQualitySetID { get; set; }
    }
}
