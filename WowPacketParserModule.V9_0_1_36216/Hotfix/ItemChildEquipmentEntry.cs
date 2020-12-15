using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemChildEquipment, HasIndexInData = false)]
    public class ItemChildEquipmentEntry
    {
        public int ParentItemID { get; set; }
        public int ChildItemID { get; set; }
        public byte ChildItemEquipSlot { get; set; }
    }
}
