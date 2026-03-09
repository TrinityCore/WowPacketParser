using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("pvp_difficulty")]
    public sealed record PvpDifficultyHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("RangeIndex")]
        public byte? RangeIndex;

        [DBFieldName("MinLevel")]
        public byte? MinLevel;

        [DBFieldName("MaxLevel")]
        public byte? MaxLevel;

        [DBFieldName("MapID")]
        public uint? MapID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
