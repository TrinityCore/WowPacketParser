using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemChildEquipment, HasIndexInData = false)]
    public class ItemChildEquipmentEntry
    {
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, false)]
        public int ParentItemID { get; set; }
        public int ChildItemID { get; set; }
        public byte ChildItemEquipSlot { get; set; }
        [HotfixVersion(ClientVersionBuild.V8_1_0_28724, true)]
        public int ParentItemId { get; set; }
    }
}
