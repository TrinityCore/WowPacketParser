using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("achievement")]
    public sealed record AchievementHotfix1000: IDataModel
    {
        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("Reward")]
        public string Reward;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("InstanceID")]
        public short? InstanceID;

        [DBFieldName("Faction")]
        public sbyte? Faction;

        [DBFieldName("Supercedes")]
        public short? Supercedes;

        [DBFieldName("Category")]
        public short? Category;

        [DBFieldName("MinimumCriteria")]
        public sbyte? MinimumCriteria;

        [DBFieldName("Points")]
        public sbyte? Points;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("UiOrder")]
        public short? UiOrder;

        [DBFieldName("IconFileID")]
        public int? IconFileID;

        [DBFieldName("RewardItemID")]
        public int? RewardItemID;

        [DBFieldName("CriteriaTree")]
        public uint? CriteriaTree;

        [DBFieldName("SharesCriteria")]
        public short? SharesCriteria;

        [DBFieldName("CovenantID")]
        public int? CovenantID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("achievement_locale")]
    public sealed record AchievementLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Title_lang")]
        public string TitleLang;

        [DBFieldName("Reward_lang")]
        public string RewardLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("achievement")]
    public sealed record AchievementHotfix340: IDataModel
    {
        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("Reward")]
        public string Reward;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("InstanceID")]
        public short? InstanceID;

        [DBFieldName("Faction")]
        public sbyte? Faction;

        [DBFieldName("Supercedes")]
        public short? Supercedes;

        [DBFieldName("Category")]
        public short? Category;

        [DBFieldName("MinimumCriteria")]
        public sbyte? MinimumCriteria;

        [DBFieldName("Points")]
        public sbyte? Points;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("UiOrder")]
        public short? UiOrder;

        [DBFieldName("IconFileID")]
        public int? IconFileID;

        [DBFieldName("CriteriaTree")]
        public uint? CriteriaTree;

        [DBFieldName("SharesCriteria")]
        public short? SharesCriteria;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("achievement_locale")]
    public sealed record AchievementLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Title_lang")]
        public string TitleLang;

        [DBFieldName("Reward_lang")]
        public string RewardLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("achievement")]
    public sealed record AchievementHotfix343: IDataModel
    {
        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("Title")]
        public string Title;

        [DBFieldName("Reward")]
        public string Reward;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("InstanceID")]
        public short? InstanceID;

        [DBFieldName("Faction")]
        public sbyte? Faction;

        [DBFieldName("Supercedes")]
        public short? Supercedes;

        [DBFieldName("Category")]
        public short? Category;

        [DBFieldName("MinimumCriteria")]
        public sbyte? MinimumCriteria;

        [DBFieldName("Points")]
        public sbyte? Points;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("UiOrder")]
        public short? UiOrder;

        [DBFieldName("IconFileID")]
        public int? IconFileID;

        [DBFieldName("CriteriaTree")]
        public uint? CriteriaTree;

        [DBFieldName("SharesCriteria")]
        public short? SharesCriteria;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("achievement_locale")]
    public sealed record AchievementLocaleHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("Title_lang")]
        public string TitleLang;

        [DBFieldName("Reward_lang")]
        public string RewardLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
