using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Campaign)]
    public class CampaignEntry
    {
        public string Title { get; set; }
        public string FactionTitle { get; set; }
        public string Description { get; set; }
        public uint ID { get; set; }
        public uint UiTextureKitID { get; set; }
        public uint RewardQuestID { get; set; }
        public uint PlayerConditionID { get; set; }
    }
}
