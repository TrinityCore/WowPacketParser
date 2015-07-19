using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_casting_requirements")]
    public sealed class SpellCastingRequirements
    {
        [DBFieldName("FacingCasterFlags")]
        public uint FacingCasterFlags;

        [DBFieldName("MinFactionID")]
        public uint MinFactionID;

        [DBFieldName("MinReputation")]
        public uint MinReputation;

        [DBFieldName("RequiredAreasID")]
        public uint RequiredAreasID;

        [DBFieldName("RequiredAuraVision")]
        public uint RequiredAuraVision;

        [DBFieldName("RequiresSpellFocus")]
        public uint RequiresSpellFocus;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
