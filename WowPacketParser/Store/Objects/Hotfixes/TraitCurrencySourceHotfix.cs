using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("trait_currency_source")]
    public sealed record TraitCurrencySourceHotfix1000: IDataModel
    {
        [DBFieldName("Requirement")]
        public string Requirement;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitCurrencyID")]
        public int? TraitCurrencyID;

        [DBFieldName("Amount")]
        public int? Amount;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("PlayerLevel")]
        public int? PlayerLevel;

        [DBFieldName("TraitNodeEntryID")]
        public int? TraitNodeEntryID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_currency_source_locale")]
    public sealed record TraitCurrencySourceLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Requirement_lang")]
        public string RequirementLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_currency_source")]
    public sealed record TraitCurrencySourceHotfix341: IDataModel
    {
        [DBFieldName("Requirement")]
        public string Requirement;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("TraitCurrencyID")]
        public int? TraitCurrencyID;

        [DBFieldName("Amount")]
        public int? Amount;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("PlayerLevel")]
        public int? PlayerLevel;

        [DBFieldName("TraitNodeEntryID")]
        public int? TraitNodeEntryID;

        [DBFieldName("OrderIndex")]
        public int? OrderIndex;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("trait_currency_source_locale")]
    public sealed record TraitCurrencySourceLocaleHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Requirement_lang")]
        public string RequirementLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
