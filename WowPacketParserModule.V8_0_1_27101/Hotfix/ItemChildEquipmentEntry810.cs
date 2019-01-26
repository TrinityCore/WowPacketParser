using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_1_0_28724.Hotfix
{
    [HotfixStructure(DB2Hash.ItemChildEquipment, ClientVersionBuild.V8_1_0_28724, HasIndexInData = false)]
    public class ItemChildEquipmentEntry
    {
        public int ParentItemID { get; set; }
        public int ChildItemID { get; set; }
        public byte ChildItemEquipSlot { get; set; }
    }
}
