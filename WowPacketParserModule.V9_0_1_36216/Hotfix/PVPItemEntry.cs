using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.PvpItem, HasIndexInData = false)]
    public class PVPItemEntry
    {
        public int ItemID { get; set; }
        public byte ItemLevelDelta { get; set; }
    }
}
