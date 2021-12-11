using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_areatrigger_splines", TargetedDatabase.Zero, TargetedDatabase.Shadowlands)]
    [DBTableName("areatrigger_create_properties_spline_point", TargetedDatabase.Shadowlands)]
    public sealed record AreaTriggerCreatePropertiesSplinePoint : IDataModel
    {
        [DBFieldName("SpellMiscId", TargetedDatabase.Zero, TargetedDatabase.Shadowlands, true)]
        [DBFieldName("AreaTriggerCreatePropertiesId", TargetedDatabase.Shadowlands, true)]
        public uint? AreaTriggerCreatePropertiesId;

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
