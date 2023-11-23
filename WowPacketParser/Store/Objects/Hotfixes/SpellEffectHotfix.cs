using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_effect")]
    public sealed record SpellEffectHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("EffectAura")]
        public short? EffectAura;

        [DBFieldName("DifficultyID")]
        public int? DifficultyID;

        [DBFieldName("EffectIndex")]
        public int? EffectIndex;

        [DBFieldName("Effect")]
        public uint? Effect;

        [DBFieldName("EffectAmplitude")]
        public float? EffectAmplitude;

        [DBFieldName("EffectAttributes")]
        public int? EffectAttributes;

        [DBFieldName("EffectAuraPeriod")]
        public int? EffectAuraPeriod;

        [DBFieldName("EffectBonusCoefficient")]
        public float? EffectBonusCoefficient;

        [DBFieldName("EffectChainAmplitude")]
        public float? EffectChainAmplitude;

        [DBFieldName("EffectChainTargets")]
        public int? EffectChainTargets;

        [DBFieldName("EffectItemType")]
        public int? EffectItemType;

        [DBFieldName("EffectMechanic")]
        public int? EffectMechanic;

        [DBFieldName("EffectPointsPerResource")]
        public float? EffectPointsPerResource;

        [DBFieldName("EffectPosFacing")]
        public float? EffectPosFacing;

        [DBFieldName("EffectRealPointsPerLevel")]
        public float? EffectRealPointsPerLevel;

        [DBFieldName("EffectTriggerSpell")]
        public int? EffectTriggerSpell;

        [DBFieldName("BonusCoefficientFromAP")]
        public float? BonusCoefficientFromAP;

        [DBFieldName("PvpMultiplier")]
        public float? PvpMultiplier;

        [DBFieldName("Coefficient")]
        public float? Coefficient;

        [DBFieldName("Variance")]
        public float? Variance;

        [DBFieldName("ResourceCoefficient")]
        public float? ResourceCoefficient;

        [DBFieldName("GroupSizeBasePointsCoefficient")]
        public float? GroupSizeBasePointsCoefficient;

        [DBFieldName("EffectBasePoints")]
        public float? EffectBasePoints;

        [DBFieldName("ScalingClass")]
        public int? ScalingClass;

        [DBFieldName("EffectMiscValue", 2)]
        public int?[] EffectMiscValue;

        [DBFieldName("EffectRadiusIndex", 2)]
        public uint?[] EffectRadiusIndex;

        [DBFieldName("EffectSpellClassMask", 4)]
        public int?[] EffectSpellClassMask;

        [DBFieldName("ImplicitTarget", 2)]
        public short?[] ImplicitTarget;

        [DBFieldName("SpellID")]
        public uint? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_effect")]
    public sealed record SpellEffectHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("DifficultyID")]
        public int? DifficultyID;

        [DBFieldName("EffectIndex")]
        public int? EffectIndex;

        [DBFieldName("Effect")]
        public uint? Effect;

        [DBFieldName("EffectAmplitude")]
        public float? EffectAmplitude;

        [DBFieldName("EffectAttributes")]
        public int? EffectAttributes;

        [DBFieldName("EffectAura")]
        public short? EffectAura;

        [DBFieldName("EffectAuraPeriod")]
        public int? EffectAuraPeriod;

        [DBFieldName("EffectBasePoints")]
        public int? EffectBasePoints;

        [DBFieldName("EffectBonusCoefficient")]
        public float? EffectBonusCoefficient;

        [DBFieldName("EffectChainAmplitude")]
        public float? EffectChainAmplitude;

        [DBFieldName("EffectChainTargets")]
        public int? EffectChainTargets;

        [DBFieldName("EffectDieSides")]
        public int? EffectDieSides;

        [DBFieldName("EffectItemType")]
        public int? EffectItemType;

        [DBFieldName("EffectMechanic")]
        public int? EffectMechanic;

        [DBFieldName("EffectPointsPerResource")]
        public float? EffectPointsPerResource;

        [DBFieldName("EffectPosFacing")]
        public float? EffectPosFacing;

        [DBFieldName("EffectRealPointsPerLevel")]
        public float? EffectRealPointsPerLevel;

        [DBFieldName("EffectTriggerSpell")]
        public int? EffectTriggerSpell;

        [DBFieldName("BonusCoefficientFromAP")]
        public float? BonusCoefficientFromAP;

        [DBFieldName("PvpMultiplier")]
        public float? PvpMultiplier;

        [DBFieldName("Coefficient")]
        public float? Coefficient;

        [DBFieldName("Variance")]
        public float? Variance;

        [DBFieldName("ResourceCoefficient")]
        public float? ResourceCoefficient;

        [DBFieldName("GroupSizeBasePointsCoefficient")]
        public float? GroupSizeBasePointsCoefficient;

        [DBFieldName("EffectMiscValue", 2)]
        public int?[] EffectMiscValue;

        [DBFieldName("EffectRadiusIndex", 2)]
        public uint?[] EffectRadiusIndex;

        [DBFieldName("EffectSpellClassMask", 4)]
        public int?[] EffectSpellClassMask;

        [DBFieldName("ImplicitTarget", 2)]
        public short?[] ImplicitTarget;

        [DBFieldName("SpellID")]
        public int? SpellID;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
