using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("rand_prop_points")]
    public sealed record RandPropPointsHotfix440: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DamageReplaceStat")]
        public int? DamageReplaceStat;

        [DBFieldName("Epic", 5)]
        public uint?[] Epic;

        [DBFieldName("Superior", 5)]
        public uint?[] Superior;

        [DBFieldName("Good", 5)]
        public uint?[] Good;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
