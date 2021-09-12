using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_areatrigger_vertices")]
    public sealed class SpellAreatriggerVertices : IDataModel
    {
        [DBFieldName("SpellMiscId", true)]
        public uint? SpellMiscId;

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

        // Will be inserted as comment to facilitate SpellMiscId research
        public uint spellId = 0;

        public WowGuid areatriggerGuid;
    }
}
