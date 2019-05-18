using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemBonus, HasIndexInData = false)]
    public class ItemBonusEntry
    {
        [HotfixArray(3)]
        public int[] Value { get; set; }
        public ushort ParentItemBonusListID { get; set; }
        public byte Type { get; set; }
        public byte OrderIndex { get; set; }
    }
}
