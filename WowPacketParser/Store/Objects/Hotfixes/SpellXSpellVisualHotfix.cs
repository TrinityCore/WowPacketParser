using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_x_spell_visual")]
    public sealed record SpellXSpellVisualHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("SpellVisualID")]
        public uint? SpellVisualID;

        [DBFieldName("Probability")]
        public float? Probability;

        [DBFieldName("Flags")]
        public int? Flags;

        [DBFieldName("Priority")]
        public int? Priority;

        [DBFieldName("SpellIconFileID")]
        public int? SpellIconFileID;

        [DBFieldName("ActiveIconFileID")]
        public int? ActiveIconFileID;

        [DBFieldName("ViewerUnitConditionID")]
        public ushort? ViewerUnitConditionID;

        [DBFieldName("ViewerPlayerConditionID")]
        public uint? ViewerPlayerConditionID;

        [DBFieldName("CasterUnitConditionID")]
        public ushort? CasterUnitConditionID;

        [DBFieldName("CasterPlayerConditionID")]
        public uint? CasterPlayerConditionID;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_x_spell_visual")]
    public sealed record SpellXSpellVisualHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("SpellVisualID")]
        public uint? SpellVisualID;

        [DBFieldName("Probability")]
        public float? Probability;

        [DBFieldName("Flags")]
        public byte? Flags;

        [DBFieldName("Priority")]
        public int? Priority;

        [DBFieldName("SpellIconFileID")]
        public int? SpellIconFileID;

        [DBFieldName("ActiveIconFileID")]
        public int? ActiveIconFileID;

        [DBFieldName("ViewerUnitConditionID")]
        public ushort? ViewerUnitConditionID;

        [DBFieldName("ViewerPlayerConditionID")]
        public uint? ViewerPlayerConditionID;

        [DBFieldName("CasterUnitConditionID")]
        public ushort? CasterUnitConditionID;

        [DBFieldName("CasterPlayerConditionID")]
        public uint? CasterPlayerConditionID;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
