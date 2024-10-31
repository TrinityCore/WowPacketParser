using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("friendship_reputation")]
    public sealed record FriendshipReputationHotfix440: IDataModel
    {
        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Unknown341_0")]
        public int? Unknown341_0;

        [DBFieldName("Unknown341_1")]
        public int? Unknown341_1;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("friendship_reputation_locale")]
    public sealed record FriendshipReputationLocaleHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Description_lang")]
        public string DescriptionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
