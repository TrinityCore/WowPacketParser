using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemAppearance, HasIndexInData = false)]
    public class ItemAppearanceEntry
    {
        public byte DisplayType { get; set; }
        public int ItemDisplayInfoID { get; set; }
        public int DefaultIconFileDataID { get; set; }
        public int UiOrder { get; set; }
        public int PlayerConditionID { get; set; }
    }
}
