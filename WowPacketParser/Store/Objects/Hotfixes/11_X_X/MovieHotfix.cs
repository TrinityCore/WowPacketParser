using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("movie")]
    public sealed record MovieHotfix1100 : IDataModel
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
}
