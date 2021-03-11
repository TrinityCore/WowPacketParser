using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemBonusTreeNode, HasIndexInData = false)]
    public class ItemBonusTreeNodeEntry
    {
        public byte ItemContext { get; set; }
        public ushort ChildItemBonusTreeID { get; set; }
        public ushort ChildItemBonusListID { get; set; }
        public ushort ChildItemLevelSelectorID { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_0_5_37503, false)]
        public int ItemBonusListGroupID { get; set; }
        [HotfixVersion(ClientVersionBuild.V9_0_5_37503, false)]
        public int Field_9_0_5_37503_005 { get; set; }
        public ushort ParentItemBonusTreeID { get; set; }
    }
}
