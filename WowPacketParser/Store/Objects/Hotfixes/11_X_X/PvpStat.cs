using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("pvp_stat")]
    public sealed record PvpStatHotfix1100: IDataModel
    {
        [DBFieldName("Description")]
        public string Description;

        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("MapID")]
        public uint? MapID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("pvp_stat_locale")]
    public sealed record PvpStatLocaleHotfix1100 : IDataModel
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
