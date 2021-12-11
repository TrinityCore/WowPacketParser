using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("creature_template_model")]
    public sealed record CreatureTemplateModel : IDataModel
    {
        [DBFieldName("CreatureID", true)]
        public uint? CreatureID;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("CreatureDisplayID")]
        public uint? CreatureDisplayID;

        [DBFieldName("DisplayScale")]
        public float? DisplayScale;

        [DBFieldName("Probability")]
        public float? Probability;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
