using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("movie")]
    public sealed record MovieHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Volume")]
        public byte? Volume;

        [DBFieldName("KeyID")]
        public byte? KeyID;

        [DBFieldName("AudioFileDataID")]
        public uint? AudioFileDataID;

        [DBFieldName("SubtitleFileDataID")]
        public uint? SubtitleFileDataID;

        [DBFieldName("SubtitleFileFormat")]
        public uint? SubtitleFileFormat;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("movie")]
    public sealed record MovieHotfix1205 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Summary")]
        public string Summary;

        [DBFieldName("Volume")]
        public byte? Volume;

        [DBFieldName("KeyID")]
        public byte? KeyID;

        [DBFieldName("AudioFileDataID")]
        public uint? AudioFileDataID;

        [DBFieldName("SubtitleFileDataID")]
        public uint? SubtitleFileDataID;

        [DBFieldName("SubtitleFileFormat")]
        public uint? SubtitleFileFormat;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("movie_locale")]
    public sealed record MovieLocaleHotfix1205: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Summary_lang")]
        public string SummaryLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
