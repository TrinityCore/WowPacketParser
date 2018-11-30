using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_scaling")]
    public sealed class CreatureTemplateScaling : IDataModel
    {
        [DBFieldName("Entry", true)]
        public uint? Entry;

        [DBFieldName("DifficultyID", true)]
        public uint? DifficultyID;

        [DBFieldName("LevelScalingMin")]
        public uint? LevelScalingMin;

        [DBFieldName("LevelScalingMax")]
        public uint? LevelScalingMax;

        [DBFieldName("LevelScalingDeltaMin")]
        public int? LevelScalingDeltaMin;

        [DBFieldName("LevelScalingDeltaMax")]
        public int? LevelScalingDeltaMax;

        [DBFieldName("SandboxScalingID", TargetedDatabase.Legion, TargetedDatabase.BattleForAzeroth)]
        [DBFieldName("ContentTuningID", TargetedDatabase.BattleForAzeroth)]
        public uint? SandboxScalingID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
