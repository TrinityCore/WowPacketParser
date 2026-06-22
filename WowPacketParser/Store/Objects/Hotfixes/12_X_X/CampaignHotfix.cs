using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("campaign")]
    public sealed record CampaignHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("UiTextureKitID")]
        public int? UiTextureKitID;

        [DBFieldName("RewardQuestID")]
        public int? RewardQuestID;

        [DBFieldName("Prerequisite")]
        public int? Prerequisite;

        [DBFieldName("Stalled")]
        public int? ValidityConditionId;

        [DBFieldName("Completed")]
        public int? Completed;

        [DBFieldName("OnlyStallIf")]
        public int? IsJourneyConditionId;

        [DBFieldName("UiQuestDetailsThemeID")]
        public int? UiQuestDetailsThemeID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("DisplayPriority")]
        public int? DisplayPriority;

        [DBFieldName("SortAsNormalQuest")]
        public int? SortAsNormalQuest;

        [DBFieldName("UseMinimalHeader")]
        public int? UseMinimalHeader;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("campaign_locale")]
    public sealed record CampaignLocaleHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Title_lang")]
        public string TitleLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
