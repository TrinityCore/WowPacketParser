using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_scaling")]
    public sealed record SpellScalingHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("Class")]
        public int? Class;

        [DBFieldName("MinScalingLevel")]
        public uint? MinScalingLevel;

        [DBFieldName("MaxScalingLevel")]
        public uint? MaxScalingLevel;

        [DBFieldName("ScalesFromItemLevel")]
        public short? ScalesFromItemLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
    [Hotfix]
    [DBTableName("spell_scaling")]
    public sealed record SpellScalingHotfix344: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("Class")]
        public int? Class;

        [DBFieldName("MinScalingLevel")]
        public uint? MinScalingLevel;

        [DBFieldName("MaxScalingLevel")]
        public uint? MaxScalingLevel;

        [DBFieldName("ScalesFromItemLevel")]
        public short? ScalesFromItemLevel;

        [DBFieldName("CastTimeMin")]
        public int? CastTimeMin;

        [DBFieldName("CastTimeMax")]
        public int? CastTimeMax;

        [DBFieldName("CastTimeMaxLevel")]
        public int? CastTimeMaxLevel;

        [DBFieldName("NerfFactor")]
        public float? NerfFactor;

        [DBFieldName("NerfMaxLevel")]
        public int? NerfMaxLevel;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
