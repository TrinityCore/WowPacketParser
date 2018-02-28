using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_scaling")]
    public sealed class CreatureTemplateScaling : IDataModel
    {
        [DBFieldName("Entry", true)]
        public uint? Entry;

        [DBFieldName("LevelScalingMin")]
        public uint? LevelScalingMin;

        [DBFieldName("LevelScalingMax")]
        public uint? LevelScalingMax;

        [DBFieldName("LevelScalingDeltaMin")]
        public int? LevelScalingDeltaMin;

        [DBFieldName("LevelScalingDeltaMax")]
        public int? LevelScalingDeltaMax;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
