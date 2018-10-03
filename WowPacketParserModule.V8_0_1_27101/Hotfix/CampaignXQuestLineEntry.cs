using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CampaignXQuestLine, HasIndexInData = false)]
    public class CampaignXQuestLineEntry
    {
        public uint CampaignID { get; set; }
        public uint QuestLineID { get; set; }
        public uint OrderIndex { get; set; }
    }
}
