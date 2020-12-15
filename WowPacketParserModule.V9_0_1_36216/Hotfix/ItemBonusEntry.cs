using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemBonus, HasIndexInData = false)]
    public class ItemBonusEntry
    {
        [HotfixArray(4)]
        public int[] Value { get; set; }
        public ushort ParentItemBonusListID { get; set; }
        public byte Type { get; set; }
        public byte OrderIndex { get; set; }
    }
}
