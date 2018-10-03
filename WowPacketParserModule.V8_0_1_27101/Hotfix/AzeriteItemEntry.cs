using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AzeriteItem, HasIndexInData = false)]
    public class AzeriteItemEntry
    {
        public int ItemID { get; set; }
    }
}
