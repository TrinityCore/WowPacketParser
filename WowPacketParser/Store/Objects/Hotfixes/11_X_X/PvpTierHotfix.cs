using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("pvp_tier")]
    public sealed record PvpTierHotfix1100: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MinRating")]
        public short? MinRating;

        [DBFieldName("MaxRating")]
        public short? MaxRating;

        [DBFieldName("PrevTier")]
        public int? PrevTier;

        [DBFieldName("NextTier")]
        public int? NextTier;

        [DBFieldName("BracketID")]
        public byte? BracketID;

        [DBFieldName("Rank")]
        public sbyte? Rank;

        [DBFieldName("RankIconFileDataID")]
        public int? RankIconFileDataID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("pvp_tier_locale")]
    public sealed record PvpTierLocaleHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
