using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("campaign_x_quest_line")]
    public sealed record CampaignXQuestLineHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CampaignID")]
        public uint? CampaignID;

        [DBFieldName("QuestLineID")]
        public uint? QuestLineID;

        [DBFieldName("OrderIndex")]
        public uint? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
