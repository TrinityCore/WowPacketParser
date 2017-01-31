using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemChildEquipment, HasIndexInData = false)]
    public class ItemChildEquipmentEntry
    {
        public uint ItemID { get; set; }
        public uint AltItemID { get; set; }
        public byte AltEquipmentSlot { get; set; }
    }
}
