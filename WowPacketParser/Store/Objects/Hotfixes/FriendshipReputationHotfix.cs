using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("friendship_reputation")]
    public sealed record FriendshipReputationHotfix1000: IDataModel
    {
        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("StandingModified")]
        public string StandingModified;

        [DBFieldName("StandingChanged")]
        public string StandingChanged;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("FactionID")]
        public int? FactionID;

        [DBFieldName("TextureFileID")]
        public int? TextureFileID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("friendship_reputation_locale")]
    public sealed record FriendshipReputationLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("StandingModified_lang")]
        public string StandingModifiedLang;

        [DBFieldName("StandingChanged_lang")]
        public string StandingChangedLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
