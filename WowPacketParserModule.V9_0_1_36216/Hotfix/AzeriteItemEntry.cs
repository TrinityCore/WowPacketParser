using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteItem, HasIndexInData = false)]
    public class AzeriteItemEntry
    {
        public int ItemID { get; set; }
    }
}
