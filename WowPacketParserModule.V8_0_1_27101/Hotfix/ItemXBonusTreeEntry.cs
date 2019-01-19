using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemXBonusTree, HasIndexInData = false)]
    public class ItemXBonusTreeEntry
    {
        public ushort ItemBonusTreeID { get; set; }
        public int ItemID { get; set; }
    }
}
