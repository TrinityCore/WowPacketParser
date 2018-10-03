using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemBonusTreeNode, HasIndexInData = false)]
    public class ItemBonusTreeNodeEntry
    {
        public byte ItemContext { get; set; }
        public ushort ChildItemBonusTreeID { get; set; }
        public ushort ChildItemBonusListID { get; set; }
        public ushort ChildItemLevelSelectorID { get; set; }
        public ushort ParentItemBonusTreeID { get; set; }
    }
}
