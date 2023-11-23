using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_target_restrictions")]
    public sealed record SpellTargetRestrictionsHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("ConeDegrees")]
        public float? ConeDegrees;

        [DBFieldName("MaxTargets")]
        public byte? MaxTargets;

        [DBFieldName("MaxTargetLevel")]
        public uint? MaxTargetLevel;

        [DBFieldName("TargetCreatureType")]
        public short? TargetCreatureType;

        [DBFieldName("Targets")]
        public int? Targets;

        [DBFieldName("Width")]
        public float? Width;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_target_restrictions")]
    public sealed record SpellTargetRestrictionsHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("ConeDegrees")]
        public float? ConeDegrees;

        [DBFieldName("MaxTargets")]
        public byte? MaxTargets;

        [DBFieldName("MaxTargetLevel")]
        public uint? MaxTargetLevel;

        [DBFieldName("TargetCreatureType")]
        public short? TargetCreatureType;

        [DBFieldName("Targets")]
        public int? Targets;

        [DBFieldName("Width")]
        public float? Width;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
