using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.QuestLineXQuest, HasIndexInData = false)]
    public class QuestLineXQuestEntry
    {
        public uint QuestLineID { get; set; }
        public uint QuestID { get; set; }
        public uint OrderIndex { get; set; }
    }
}
