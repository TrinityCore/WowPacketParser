using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemXItemEffect, HasIndexInData = false)]
    public class ItemXItemEffectEntry
    {
        public int ItemEffectID { get; set; }
        public int ItemID { get; set; }
    }
}
