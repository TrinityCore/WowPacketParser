using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("content_tuning_x_expected")]
    public sealed record ContentTuningXExpectedHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ExpectedStatModID")]
        public int? ExpectedStatModID;

        [DBFieldName("MinMythicPlusSeasonID")]
        public int? MinMythicPlusSeasonID;

        [DBFieldName("MaxMythicPlusSeasonID")]
        public int? MaxMythicPlusSeasonID;

        [DBFieldName("ContentTuningID")]
        public uint? ContentTuningID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
