using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.QuestLine, HasIndexInData = false)]
    public class QuestLineEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint QuestID { get; set; }
    }
}
