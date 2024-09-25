using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_areatrigger_vertices", TargetedDatabaseFlag.TillBattleForAzeroth)]
    [DBTableName("areatrigger_create_properties_polygon_vertex", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
    public sealed record AreaTriggerCreatePropertiesPolygonVertex : IDataModel
    {
        [DBFieldName("SpellMiscId", TargetedDatabaseFlag.TillBattleForAzeroth, true)]
        [DBFieldName("AreaTriggerCreatePropertiesId", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic, true)]
        public uint? AreaTriggerCreatePropertiesId;

        [DBFieldName("IsCustom", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic, true)]
        public byte? IsCustom = 1;

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

    [DBTableName("spell_areatrigger_vertices", TargetedDatabaseFlag.TillBattleForAzeroth)]
    [DBTableName("areatrigger_create_properties_polygon_vertex", TargetedDatabaseFlag.SinceShadowlands)]
    public sealed record AreaTriggerCreatePropertiesPolygonVertexCustom : IDataModel
    {
        [DBFieldName("SpellMiscId", TargetedDatabaseFlag.TillBattleForAzeroth, true, true)]
        [DBFieldName("AreaTriggerCreatePropertiesId", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic, true, true)]
        public string AreaTriggerCreatePropertiesId;

        [DBFieldName("IsCustom", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic, true)]
        public byte? IsCustom;

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
    }
}
