using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_scaling", TargetedDatabaseFlag.TillShadowlands)]
    [DBTableName("creature_template_difficulty", TargetedDatabaseFlag.SinceDragonflight)]
    public sealed record CreatureTemplateDifficulty : IDataModel
    {
        [DBFieldName("Entry", true)]
        public uint? Entry;

        [DBFieldName("DifficultyID", true)]
        public uint? DifficultyID;

        [DBFieldName("LevelScalingMin", TargetedDatabaseFlag.Legion | TargetedDatabaseFlag.BattleForAzeroth)]
        public uint? LevelScalingMin;

        [DBFieldName("LevelScalingMax", TargetedDatabaseFlag.Legion | TargetedDatabaseFlag.BattleForAzeroth)]
        public uint? LevelScalingMax;

        [DBFieldName("LevelScalingDeltaMin")]
        public int? LevelScalingDeltaMin;

        [DBFieldName("LevelScalingDeltaMax")]
        public int? LevelScalingDeltaMax;

        [DBFieldName("SandboxScalingID", TargetedDatabaseFlag.Legion)]
        [DBFieldName("ContentTuningID", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public int? ContentTuningID;

        [DBFieldName("HealthScalingExpansion", TargetedDatabaseFlag.SinceDragonflight)]
        public ClientType? HealthScalingExpansion;

        [DBFieldName("HealthModifier", TargetedDatabaseFlag.SinceDragonflight)]
        public float? HealthModifier;

        [DBFieldName("ManaModifier", TargetedDatabaseFlag.SinceDragonflight)]
        public float? ManaModifier;

        [DBFieldName("CreatureDifficultyID", TargetedDatabaseFlag.SinceDragonflight)]
        public int? CreatureDifficultyID;

        [DBFieldName("TypeFlags", TargetedDatabaseFlag.SinceDragonflight)]
        public CreatureTypeFlag? TypeFlags;

        [DBFieldName("TypeFlags2", TargetedDatabaseFlag.SinceDragonflight)]
        public uint? TypeFlags2;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
