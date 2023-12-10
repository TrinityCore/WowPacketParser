using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_scaling", TargetedDatabaseFlag.TillShadowlands)]
    [DBTableName("creature_template_difficulty", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic)]
    public sealed record CreatureTemplateDifficultyWDB : IDataModel
    {
        [DBFieldName("Entry", true)]
        public uint? Entry;

        [DBFieldName("DifficultyID", true)]
        public uint? DifficultyID;

        [DBFieldName("HealthScalingExpansion", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic)]
        public ClientType? HealthScalingExpansion;

        [DBFieldName("HealthModifier", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic)]
        public float? HealthModifier;

        [DBFieldName("ManaModifier", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic)]
        public float? ManaModifier;

        [DBFieldName("CreatureDifficultyID", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic)]
        public int? CreatureDifficultyID;

        [DBFieldName("TypeFlags", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic)]
        public CreatureTypeFlag? TypeFlags;

        [DBFieldName("TypeFlags2", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic)]
        public uint? TypeFlags2;
    }

    [DBTableName("creature_template_scaling", TargetedDatabaseFlag.TillShadowlands)]
    [DBTableName("creature_template_difficulty", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.WotlkClassic)]
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

        [DBFieldName("LevelScalingDeltaMin", TargetedDatabaseFlag.AnyRetail)]
        public int? LevelScalingDeltaMin;

        [DBFieldName("LevelScalingDeltaMax", TargetedDatabaseFlag.AnyRetail)]
        public int? LevelScalingDeltaMax;

        [DBFieldName("MinLevel", TargetedDatabaseFlag.WotlkClassic)]
        public int? MinLevel;

        [DBFieldName("MaxLevel", TargetedDatabaseFlag.WotlkClassic)]
        public int? MaxLevel;

        [DBFieldName("SandboxScalingID", TargetedDatabaseFlag.Legion)]
        [DBFieldName("ContentTuningID", TargetedDatabaseFlag.SinceBattleForAzeroth)]
        public int? ContentTuningID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
