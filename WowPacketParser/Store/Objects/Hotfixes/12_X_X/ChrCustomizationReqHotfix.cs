using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_customization_req")]
    public sealed record ChrCustomizationReqHotfix1200 : IDataModel
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

        [DBFieldName("RegionGroupMask")]
        public int? RegionGroupMask;

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
    public sealed record ChrCustomizationReqLocaleHotfix1200 : IDataModel
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
    public sealed record ChrCustomizationReqHotfix1205 : IDataModel
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

        [DBFieldName("RegionGroupMask")]
        public int? RegionGroupMask;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("OverrideArchive")]
        public int? OverrideArchive;

        [DBFieldName("ItemModifiedAppearanceID")]
        public int? ItemModifiedAppearanceID;

        [DBFieldName("RaceMask_1")]
        public int? RaceMask_1;

        [DBFieldName("RaceMask_2")]
        public int? RaceMask_2;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_req_locale")]
    public sealed record ChrCustomizationReqLocaleHotfix1205 : IDataModel
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
    public sealed record ChrCustomizationReqHotfix1207 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ReqSource")]
        public string ReqSource;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("RegionGroupMask")]
        public int? RegionGroupMask;

        [DBFieldName("AchievementID")]
        public int? AchievementID;

        [DBFieldName("QuestID")]
        public int? QuestID;

        [DBFieldName("OverrideArchive")]
        public int? OverrideArchive;

        [DBFieldName("ItemModifiedAppearanceID")]
        public int? ItemModifiedAppearanceID;

        [DBFieldName("RaceMask", 2)]
        public int?[] RaceMask;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_req_locale")]
    public sealed record ChrCustomizationReqLocaleHotfix1207 : IDataModel
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
