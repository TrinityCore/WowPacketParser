using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_casting_requirements")]
    public sealed record SpellCastingRequirementsHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("FacingCasterFlags")]
        public int? FacingCasterFlags;

        [DBFieldName("MinFactionID")]
        public ushort? MinFactionID;

        [DBFieldName("MinReputation")]
        public int? MinReputation;

        [DBFieldName("RequiredAreasID")]
        public ushort? RequiredAreasID;

        [DBFieldName("RequiredAuraVision")]
        public byte? RequiredAuraVision;

        [DBFieldName("RequiresSpellFocus")]
        public ushort? RequiresSpellFocus;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
