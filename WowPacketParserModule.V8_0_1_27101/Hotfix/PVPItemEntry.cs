using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PvpItem, HasIndexInData = false)]
    public class PvpItemEntry
    {
        public int ItemID { get; set; }
        public byte ItemLevelDelta { get; set; }
    }
}
