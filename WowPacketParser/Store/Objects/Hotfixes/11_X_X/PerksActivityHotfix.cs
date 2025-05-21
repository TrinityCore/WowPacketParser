using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("perks_activity")]
    public sealed record PerksActivityHotfix1100: IDataModel
    {
        [DBFieldName("ActivityName")]
        public string ActivityName;

        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("CriteriaTreeID")]
        public int? CriteriaTreeID;

        [DBFieldName("ThresholdContributionAmount")]
        public int? ThresholdContributionAmount;

        [DBFieldName("Supersedes")]
        public int? Supersedes;

        [DBFieldName("Priority")]
        public int? Priority;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("perks_activity_locale")]
    public sealed record PerksActivityLocaleHotfix1100 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("ActivityName_lang")]
        public string ActivityNameLang;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
