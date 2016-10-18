using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("areatrigger_template_polygon_vertices")]
    public sealed class AreaTriggerTemplateVertices : IDataModel
    {
        [DBFieldName("AreaTriggerId", true)]
        public uint? AreaTriggerId;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("VerticeX")]
        public float? VerticeX;

        [DBFieldName("VerticeY")]
        public float? VerticeY;

        [DBFieldName("VerticeTargetX")]
        public float? VerticeTargetX;

        [DBFieldName("VerticeTargetY")]
        public float? VerticeTargetY;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
