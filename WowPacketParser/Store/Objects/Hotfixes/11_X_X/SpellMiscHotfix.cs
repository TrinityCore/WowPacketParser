using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_misc")]
    public sealed record SpellMiscHotfix1100: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Attributes", 15)]
        public int?[] Attributes;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("CastingTimeIndex")]
        public ushort? CastingTimeIndex;

        [DBFieldName("DurationIndex")]
        public ushort? DurationIndex;

        [DBFieldName("PvPDurationIndex")]
        public ushort? PvPDurationIndex;

        [DBFieldName("RangeIndex")]
        public ushort? RangeIndex;

        [DBFieldName("SchoolMask")]
        public byte? SchoolMask;

        [DBFieldName("Speed")]
        public float? Speed;

        [DBFieldName("LaunchDelay")]
        public float? LaunchDelay;

        [DBFieldName("MinDuration")]
        public float? MinDuration;

        [DBFieldName("SpellIconFileDataID")]
        public int? SpellIconFileDataID;

        [DBFieldName("ActiveIconFileDataID")]
        public int? ActiveIconFileDataID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("ShowFutureSpellPlayerConditionID")]
        public int? ShowFutureSpellPlayerConditionID;

        [DBFieldName("SpellVisualScript")]
        public int? SpellVisualScript;

        [DBFieldName("ActiveSpellVisualScript")]
        public int? ActiveSpellVisualScript;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_misc")]
    public sealed record SpellMiscHotfix1105 : IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Attributes", 16)]
        public int?[] Attributes;

        [DBFieldName("DifficultyID")]
        public byte? DifficultyID;

        [DBFieldName("CastingTimeIndex")]
        public ushort? CastingTimeIndex;

        [DBFieldName("DurationIndex")]
        public ushort? DurationIndex;

        [DBFieldName("PvPDurationIndex")]
        public ushort? PvPDurationIndex;

        [DBFieldName("RangeIndex")]
        public ushort? RangeIndex;

        [DBFieldName("SchoolMask")]
        public byte? SchoolMask;

        [DBFieldName("Speed")]
        public float? Speed;

        [DBFieldName("LaunchDelay")]
        public float? LaunchDelay;

        [DBFieldName("MinDuration")]
        public float? MinDuration;

        [DBFieldName("SpellIconFileDataID")]
        public int? SpellIconFileDataID;

        [DBFieldName("ActiveIconFileDataID")]
        public int? ActiveIconFileDataID;

        [DBFieldName("ContentTuningID")]
        public int? ContentTuningID;

        [DBFieldName("ShowFutureSpellPlayerConditionID")]
        public int? ShowFutureSpellPlayerConditionID;

        [DBFieldName("SpellVisualScript")]
        public int? SpellVisualScript;

        [DBFieldName("ActiveSpellVisualScript")]
        public int? ActiveSpellVisualScript;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
