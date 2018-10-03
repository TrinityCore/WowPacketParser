using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CharBaseInfo, HasIndexInData = false)]
    public class CharBaseInfoEntry
    {
        public sbyte RaceID { get; set; }
        public sbyte ClassID { get; set; }
    }
}
