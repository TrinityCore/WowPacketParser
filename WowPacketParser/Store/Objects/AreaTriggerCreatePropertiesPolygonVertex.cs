using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_areatrigger_vertices", TargetedDatabase.Zero, TargetedDatabase.Shadowlands)]
    [DBTableName("areatrigger_create_properties_polygon_vertex", TargetedDatabase.Shadowlands)]
    public sealed record AreaTriggerCreatePropertiesPolygonVertex : IDataModel
    {
        [DBFieldName("SpellMiscId", TargetedDatabase.Zero, TargetedDatabase.Shadowlands, true)]
        [DBFieldName("AreaTriggerCreatePropertiesId", TargetedDatabase.Shadowlands, true)]
        public uint? AreaTriggerCreatePropertiesId;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("VerticeX")]
        public float? VerticeX;

        [DBFieldName("VerticeY")]
        public float? VerticeY;

        [DBFieldName("VerticeTargetX", false, false, true)]
        public float? VerticeTargetX;

        [DBFieldName("VerticeTargetY", false, false, true)]
        public float? VerticeTargetY;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        // Will be inserted as comment
        public uint spellId = 0;

        public WowGuid areatriggerGuid;
    }
}
