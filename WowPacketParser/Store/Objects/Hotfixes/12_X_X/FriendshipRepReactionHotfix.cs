using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("friendship_rep_reaction")]
    public sealed record FriendshipRepReactionHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Reaction")]
        public string Reaction;

        [DBFieldName("FriendshipRepID")]
        public uint? FriendshipRepID;

        [DBFieldName("ReactionThreshold")]
        public int? ReactionThreshold;

        [DBFieldName("OverrideColor")]
        public int? OverrideColor;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("friendship_rep_reaction_locale")]
    public sealed record FriendshipRepReactionLocaleHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Reaction_lang")]
        public string ReactionLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
