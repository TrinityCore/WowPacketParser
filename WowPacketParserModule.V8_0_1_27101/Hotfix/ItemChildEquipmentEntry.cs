using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemChildEquipment, HasIndexInData = false)]
    public class ItemChildEquipmentEntry
    {
        public int ChildItemID { get; set; }
        public byte ChildItemEquipSlot { get; set; }
        public int ParentItemID { get; set; }
    }
}
