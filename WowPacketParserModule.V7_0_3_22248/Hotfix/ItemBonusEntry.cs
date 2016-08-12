using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemBonus, HasIndexInData = false)]
    public class ItemBonusEntry
    {
        [HotfixArray(2)]
        public int[] Value { get; set; }
        public ushort BonusListID { get; set; }
        public byte Type { get; set; }
        public byte Index { get; set; }
    }
}