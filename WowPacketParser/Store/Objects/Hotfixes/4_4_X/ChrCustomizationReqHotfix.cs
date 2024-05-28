using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("chr_customization_req")]
    public sealed record ChrCustomizationReqHotfix440: IDataModel
    {
        [DBFieldName("RaceMask")]
        public long? RaceMask;

        [DBFieldName("ReqSource")]
        public string ReqSource;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ReqType")]
        public int? ReqType;

        [DBFieldName("ClassMask")]
        public int? ClassMask;

        [DBFieldName("ReqAchievementID")]
        public int? ReqAchievementID;

        [DBFieldName("ReqQuestID")]
        public int? ReqQuestID;

        [DBFieldName("OverrideArchive")]
        public int? OverrideArchive;

        [DBFieldName("ReqItemModifiedAppearanceID")]
        public int? ReqItemModifiedAppearanceID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("chr_customization_req_locale")]
    public sealed record ChrCustomizationReqLocaleHotfix440: IDataModel
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
