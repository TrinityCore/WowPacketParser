using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_customization_req")]
    public sealed record ChrCustomizationReqHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ReqSource")]
        public string ReqSource;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("OverrideArchive")]
        public int? OverrideArchive;

        [DBFieldName("ItemModifiedAppearanceID")]
        public int? ItemModifiedAppearanceID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_req_locale")]
    public sealed record ChrCustomizationReqLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("ReqSource_lang")]
        public string ReqSourceLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_req")]
    public sealed record ChrCustomizationReqHotfix1015 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("ReqSource")]
        public string ReqSource;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("OverrideArchive")]
        public int? OverrideArchive;

        [DBFieldName("ItemModifiedAppearanceID")]
        public int? ItemModifiedAppearanceID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_req_locale")]
    public sealed record ChrCustomizationReqLocaleHotfix1015 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("ReqSource_lang")]
        public string ReqSourceLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_req")]
    public sealed record ChrCustomizationReqHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("OverrideArchive")]
        public int? OverrideArchive;

        [DBFieldName("ItemModifiedAppearanceID")]
        public int? ItemModifiedAppearanceID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("chr_customization_req")]
    public sealed record ChrCustomizationReqHotfix341: IDataModel
    {
        [DBFieldName("ReqSource")]
        public string ReqSource;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("OverrideArchive")]
        public int? OverrideArchive;

        [DBFieldName("ItemModifiedAppearanceID")]
        public int? ItemModifiedAppearanceID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_req_locale")]
    public sealed record ChrCustomizationReqLocaleHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("ReqSource_lang")]
        public string ReqSourceLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("chr_customization_req")]
    public sealed record ChrCustomizationReqHotfix343: IDataModel
    {
        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("ReqSource")]
        public string ReqSource;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("OverrideArchive")]
        public int? OverrideArchive;

        [DBFieldName("ItemModifiedAppearanceID")]
        public int? ItemModifiedAppearanceID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_req_locale")]
    public sealed record ChrCustomizationReqLocaleHotfix343: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("ReqSource_lang")]
        public string ReqSourceLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
