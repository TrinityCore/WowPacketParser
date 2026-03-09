using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_visual_kit")]
    public sealed record SpellVisualKitHotfix1200 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("ClutterLevel")]
        public int? ClutterLevel;

        [DBFieldName("FallbackSpellVisualKitId")]
        public int? FallbackSpellVisualKitId;

        [DBFieldName("DelayMin")]
        public ushort? DelayMin;

        [DBFieldName("DelayMax")]
        public ushort? DelayMax;

        [DBFieldName("MinimumSpellVisualDensityFilterType")]
        public int? MinimumSpellVisualDensityFilterType;

        [DBFieldName("MinimumSpellVisualDensityFilterParam")]
        public int? MinimumSpellVisualDensityFilterParam;

        [DBFieldName("ReducedSpellVisualDensityFilterType")]
        public int? ReducedSpellVisualDensityFilterType;

        [DBFieldName("ReducedSpellVisualDensityFilterParam")]
        public int? ReducedSpellVisualDensityFilterParam;

        [DBFieldName("Flags", 2)]
        public int?[] Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
