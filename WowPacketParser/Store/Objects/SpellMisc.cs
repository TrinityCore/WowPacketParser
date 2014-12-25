using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_misc")]
    public sealed class SpellMisc
    {
        [DBFieldName("Attributes", 14)]
        public uint[] Attributes;

        [DBFieldName("CastingTimeIndex")]
        public uint CastingTimeIndex;

        [DBFieldName("DurationIndex")]
        public uint DurationIndex;

        [DBFieldName("RangeIndex")]
        public uint RangeIndex;

        [DBFieldName("CastingSpeedTimeIndex")]
        public float Speed;

        [DBFieldName("SpellVisualID", 2)]
        public uint[] SpellVisualID;

        [DBFieldName("SpellIconID")]
        public uint SpellIconID;

        [DBFieldName("ActiveIconID")]
        public uint ActiveIconID;

        [DBFieldName("SchoolMask")]
        public uint SchoolMask;

        [DBFieldName("UnkWoD1")]
        public float UnkWoD1;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
