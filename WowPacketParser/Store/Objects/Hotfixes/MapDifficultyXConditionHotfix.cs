using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("map_difficulty_x_condition")]
    public sealed record MapDifficultyXConditionHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FailureDescription")]
        public string FailureDescription;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("MapDifficultyID")]
        public uint? MapDifficultyID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_difficulty_x_condition_locale")]
    public sealed record MapDifficultyXConditionLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("FailureDescription_lang")]
        public string FailureDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_difficulty_x_condition")]
    public sealed record MapDifficultyXConditionHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FailureDescription")]
        public string FailureDescription;

        [DBFieldName("PlayerConditionID")]
        public uint? PlayerConditionID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("MapDifficultyID")]
        public int? MapDifficultyID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_difficulty_x_condition_locale")]
    public sealed record MapDifficultyXConditionLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("FailureDescription_lang")]
        public string FailureDescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
