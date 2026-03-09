using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("content_tuning")]
    public sealed record ContentTuningHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("ExpansionID")]
        public int? ExpansionID;

        [DBFieldName("HealthItemLevelCurveID")]
        public int? HealthItemLevelCurveID;

        [DBFieldName("DamageItemLevelCurveID")]
        public int? DamageItemLevelCurveID;

        [DBFieldName("HealthPrimaryStatCurveID")]
        public int? HealthPrimaryStatCurveID;

        [DBFieldName("DamagePrimaryStatCurveID")]
        public int? DamagePrimaryStatCurveID;

        [DBFieldName("PrimaryStatScalingModPlayerDataElementCharacterID")]
        public int? PrimaryStatScalingModPlayerDataElementCharacterID;

        [DBFieldName("PrimaryStatScalingModPlayerDataElementCharacterMultiplier")]
        public float? PrimaryStatScalingModPlayerDataElementCharacterMultiplier;

        [DBFieldName("MinLevel")]
        public int? MinLevel;

        [DBFieldName("MaxLevel")]
        public int? MaxLevel;

        [DBFieldName("MinLevelType")]
        public int? MinLevelType;

        [DBFieldName("MaxLevelType")]
        public int? MaxLevelType;

        [DBFieldName("TargetLevelDelta")]
        public int? TargetLevelDelta;

        [DBFieldName("TargetLevelMaxDelta")]
        public int? TargetLevelMaxDelta;

        [DBFieldName("TargetLevelMin")]
        public int? TargetLevelMin;

        [DBFieldName("TargetLevelMax")]
        public int? TargetLevelMax;

        [DBFieldName("MinItemLevel")]
        public int? MinItemLevel;

        [DBFieldName("QuestXpMultiplier")]
        public float? QuestXpMultiplier;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
