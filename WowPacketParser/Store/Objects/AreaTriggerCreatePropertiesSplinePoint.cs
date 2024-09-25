using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_areatrigger_splines", TargetedDatabaseFlag.TillBattleForAzeroth)]
    [DBTableName("areatrigger_create_properties_spline_point", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
    public sealed record AreaTriggerCreatePropertiesSplinePoint : IDataModel
    {
        [DBFieldName("SpellMiscId", TargetedDatabaseFlag.TillBattleForAzeroth, true, true)]
        [DBFieldName("AreaTriggerCreatePropertiesId", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic, true, true)]
        public uint? AreaTriggerCreatePropertiesId;

        [DBFieldName("IsCustom", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic, true)]
        public byte? IsCustom;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        [DBFieldName("X")]
        public float? X;

        [DBFieldName("Y")]
        public float? Y;

        [DBFieldName("Z")]
        public float? Z;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        // Will be inserted as comment
        public uint spellId = 0;

        public WowGuid areatriggerGuid;
    }
}
