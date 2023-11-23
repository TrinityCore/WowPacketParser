using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_power")]
    public sealed record SpellPowerHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("ManaCost")]
        public int? ManaCost;

        [DBFieldName("ManaCostPerLevel")]
        public int? ManaCostPerLevel;

        [DBFieldName("ManaPerSecond")]
        public int? ManaPerSecond;

        [DBFieldName("PowerDisplayID")]
        public uint? PowerDisplayID;

        [DBFieldName("AltPowerBarID")]
        public int? AltPowerBarID;

        [DBFieldName("PowerCostPct")]
        public float? PowerCostPct;

        [DBFieldName("PowerCostMaxPct")]
        public float? PowerCostMaxPct;

        [DBFieldName("OptionalCostPct")]
        public float? OptionalCostPct;

        [DBFieldName("PowerPctPerSecond")]
        public float? PowerPctPerSecond;

        [DBFieldName("PowerType")]
        public sbyte? PowerType;

        [DBFieldName("RequiredAuraSpellID")]
        public int? RequiredAuraSpellID;

        [DBFieldName("OptionalCost")]
        public uint? OptionalCost;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_power")]
    public sealed record SpellPowerHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("OrderIndex")]
        public byte? OrderIndex;

        [DBFieldName("ManaCost")]
        public int? ManaCost;

        [DBFieldName("ManaCostPerLevel")]
        public int? ManaCostPerLevel;

        [DBFieldName("ManaPerSecond")]
        public int? ManaPerSecond;

        [DBFieldName("PowerDisplayID")]
        public uint? PowerDisplayID;

        [DBFieldName("AltPowerBarID")]
        public int? AltPowerBarID;

        [DBFieldName("PowerCostPct")]
        public float? PowerCostPct;

        [DBFieldName("PowerCostMaxPct")]
        public float? PowerCostMaxPct;

        [DBFieldName("PowerPctPerSecond")]
        public float? PowerPctPerSecond;

        [DBFieldName("PowerType")]
        public sbyte? PowerType;

        [DBFieldName("RequiredAuraSpellID")]
        public int? RequiredAuraSpellID;

        [DBFieldName("OptionalCost")]
        public uint? OptionalCost;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
