using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.QuestXGroupActivity, HasIndexInData = false)]
    public class QuestXGroupActivityEntry
    {
        public uint QuestID { get; set; }
        public uint GroupFinderActivityID { get; set; }
    }
}
