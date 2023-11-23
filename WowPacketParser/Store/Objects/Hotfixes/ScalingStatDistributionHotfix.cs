using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("scaling_stat_distribution")]
    public sealed record ScalingStatDistributionHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PlayerLevelToItemLevelCurveID")]
        public ushort? PlayerLevelToItemLevelCurveID;

        [DBFieldName("MinLevel")]
        public int? MinLevel;

        [DBFieldName("MaxLevel")]
        public int? MaxLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("scaling_stat_distribution")]
    public sealed record ScalingStatDistributionHotfix341: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("PlayerLevelToItemLevelCurveID")]
        public ushort? PlayerLevelToItemLevelCurveID;

        [DBFieldName("MinLevel")]
        public int? MinLevel;

        [DBFieldName("MaxLevel")]
        public int? MaxLevel;

        [DBFieldName("Bonus", 10)]
        public int?[] Bonus;

        [DBFieldName("StatID", 10)]
        public int?[] StatID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
