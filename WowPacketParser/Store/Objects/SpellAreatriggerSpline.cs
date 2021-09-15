using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_areatrigger_splines")]
    public sealed class SpellAreatriggerSpline : IDataModel
    {
        [DBFieldName("SpellMiscId", true)]
        public uint? SpellMiscId;

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
