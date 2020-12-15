using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemXBonusTree, HasIndexInData = false)]
    public class ItemXBonusTreeEntry
    {
        public ushort ItemBonusTreeID { get; set; }
        public uint ItemID { get; set; }
    }
}
