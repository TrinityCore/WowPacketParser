using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_misc")]
    public sealed class SpellMisc
    {
        [DBFieldName("Attributes")]
        public uint Attributes;

        [DBFieldName("AttributesEx")]
        public uint AttributesEx;

        [DBFieldName("AttributesExB")]
        public uint AttributesExB;

        [DBFieldName("AttributesExC")]
        public uint AttributesExC;

        [DBFieldName("AttributesExD")]
        public uint AttributesExD;

        [DBFieldName("AttributesExE")]
        public uint AttributesExE;

        [DBFieldName("AttributesExF")]
        public uint AttributesExF;

        [DBFieldName("AttributesExG")]
        public uint AttributesExG;

        [DBFieldName("AttributesExH")]
        public uint AttributesExH;

        [DBFieldName("AttributesExI")]
        public uint AttributesExI;

        [DBFieldName("AttributesExJ")]
        public uint AttributesExJ;

        [DBFieldName("AttributesExK")]
        public uint AttributesExK;

        [DBFieldName("AttributesExL")]
        public uint AttributesExL;

        [DBFieldName("AttributesExM")]
        public uint AttributesExM;

        [DBFieldName("CastingTimeIndex")]
        public uint CastingTimeIndex;

        [DBFieldName("DurationIndex")]
        public uint DurationIndex;

        [DBFieldName("RangeIndex")]
        public uint RangeIndex;

        [DBFieldName("Speed")]
        public float Speed;

        [DBFieldName("SpellVisualID", 2)]
        public uint[] SpellVisualID;

        [DBFieldName("SpellIconID")]
        public uint SpellIconID;

        [DBFieldName("ActiveIconID")]
        public uint ActiveIconID;

        [DBFieldName("SchoolMask")]
        public uint SchoolMask;

        [DBFieldName("MultistrikeSpeedMod")]
        public float MultistrikeSpeedMod;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
