using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_scaling")]
    public sealed class CreatureTemplateScaling : IDataModel
    {
        [DBFieldName("entry", true)]
        public uint? entry;

        [DBFieldName("levelScalingDelta")]
        public uint? levelScalingDelta;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
