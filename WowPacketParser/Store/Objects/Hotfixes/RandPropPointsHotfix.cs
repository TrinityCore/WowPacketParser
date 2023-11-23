using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("rand_prop_points")]
    public sealed record RandPropPointsHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DamageReplaceStatF")]
        public float? DamageReplaceStatF;

        [DBFieldName("DamageSecondaryF")]
        public float? DamageSecondaryF;

        [DBFieldName("DamageReplaceStat")]
        public int? DamageReplaceStat;

        [DBFieldName("DamageSecondary")]
        public int? DamageSecondary;

        [DBFieldName("EpicF", 5)]
        public float?[] EpicF;

        [DBFieldName("SuperiorF", 5)]
        public float?[] SuperiorF;

        [DBFieldName("GoodF", 5)]
        public float?[] GoodF;

        [DBFieldName("Epic", 5)]
        public uint?[] Epic;

        [DBFieldName("Superior", 5)]
        public uint?[] Superior;

        [DBFieldName("Good", 5)]
        public uint?[] Good;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("rand_prop_points")]
    public sealed record RandPropPointsHotfix340: IDataModel
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
