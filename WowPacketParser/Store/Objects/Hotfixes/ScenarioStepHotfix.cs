using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("scenario_step")]
    public sealed record ScenarioStepHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("ScenarioID")]
        public ushort? ScenarioID;

        [DBFieldName("Criteriatreeid")]
        public uint? Criteriatreeid;

        [DBFieldName("RewardQuestID")]
        public int? RewardQuestID;

        [DBFieldName("RelatedStep")]
        public int? RelatedStep;

        [DBFieldName("Supersedes")]
        public ushort? Supersedes;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("VisibilityPlayerConditionID")]
        public uint? VisibilityPlayerConditionID;

        [DBFieldName("WidgetSetID")]
        public ushort? WidgetSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("scenario_step_locale")]
    public sealed record ScenarioStepLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Title_lang")]
        public string TitleLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("scenario_step")]
    public sealed record ScenarioStepHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("ScenarioID")]
        public ushort? ScenarioID;

        [DBFieldName("CriteriatreeID")]
        public uint? CriteriatreeID;

        [DBFieldName("RewardQuestID")]
        public uint? RewardQuestID;

        [DBFieldName("RelatedStep")]
        public int? RelatedStep;

        [DBFieldName("Supersedes")]
        public ushort? Supersedes;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("VisibilityPlayerConditionID")]
        public uint? VisibilityPlayerConditionID;

        [DBFieldName("WidgetSetID")]
        public ushort? WidgetSetID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("scenario_step_locale")]
    public sealed record ScenarioStepLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Title_lang")]
        public string TitleLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
