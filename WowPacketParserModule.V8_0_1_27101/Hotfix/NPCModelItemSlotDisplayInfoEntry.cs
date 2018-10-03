using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.NpcModelItemSlotDisplayInfo, HasIndexInData = false)]
    public class NpcModelItemSlotDisplayInfoEntry
    {
        public int ItemDisplayInfoID { get; set; }
        public sbyte ItemSlot { get; set; }
        public int NpcModelID { get; set; }
    }
}
