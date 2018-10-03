using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PvpBracketTypes, HasIndexInData = false)]
    public class PvpBracketTypesEntry
    {
        public sbyte BracketID { get; set; }
        [HotfixArray(4)]
        public uint[] WeeklyQuestID { get; set; }
    }
}
