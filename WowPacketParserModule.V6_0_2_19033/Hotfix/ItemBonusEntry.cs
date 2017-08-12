using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.ItemBonus)]
    public class ItemBonusEntry
    {
        public int ID { get; set; }
        public int BonusListID { get; set; }
        public int Type { get; set; }
        [HotfixArray(2)]
        public int[] Value { get; set; }
        public int Index { get; set; }
    }
}
