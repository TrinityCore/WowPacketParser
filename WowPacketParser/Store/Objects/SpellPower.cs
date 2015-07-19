using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_power")]
    public sealed class SpellPower
    {
        [DBFieldName("SpellID")]
        public uint SpellID;

        [DBFieldName("PowerIndex")]
        public uint PowerIndex;

        [DBFieldName("ManaCost")]
        public uint ManaCost;

        [DBFieldName("ManaCostPerLevel")]
        public uint ManaCostPerLevel;

        [DBFieldName("ManaCostPerSecond")]
        public uint ManaCostPerSecond;

        [DBFieldName("ManaCostAdditional")]
        public uint ManaCostAdditional;

        [DBFieldName("PowerDisplayID")]
        public uint PowerDisplayID;

        [DBFieldName("UnitPowerBarID")]
        public uint UnitPowerBarID;

        [DBFieldName("ManaCostPercentage")]
        public float ManaCostPercentage;

        [DBFieldName("ManaCostPercentagePerSecond")]
        public float ManaCostPercentagePerSecond;

        [DBFieldName("RequiredAura")]
        public uint RequiredAura;

        [DBFieldName("HealthCostPercentage")]
        public float HealthCostPercentage;

        [DBFieldName("VerifiedBuild")]
        public int VerifiedBuild = ClientVersion.BuildInt;
    }
}
