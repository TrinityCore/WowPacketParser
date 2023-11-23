using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("map_challenge_mode")]
    public sealed record MapChallengeModeHotfix1000: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ExpansionLevel")]
        public uint? ExpansionLevel;

        [DBFieldName("RequiredWorldStateID")]
        public int? RequiredWorldStateID;

        [DBFieldName("CriteriaCount", 3)]
        public short?[] CriteriaCount;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_challenge_mode_locale")]
    public sealed record MapChallengeModeLocaleHotfix1000: IDataModel
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

    [Hotfix]
    [DBTableName("map_challenge_mode")]
    public sealed record MapChallengeModeHotfix340: IDataModel
    {
        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public ushort? MapID;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("ExpansionLevel")]
        public uint? ExpansionLevel;

        [DBFieldName("RequiredWorldStateID")]
        public int? RequiredWorldStateID;

        [DBFieldName("CriteriaCount", 3)]
        public short?[] CriteriaCount;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("map_challenge_mode_locale")]
    public sealed record MapChallengeModeLocaleHotfix340: IDataModel
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
