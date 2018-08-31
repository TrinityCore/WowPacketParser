using System.Runtime.InteropServices;
namespace WowPacketParser.DBC.Structures.Legion
{
    [DBFile("SpellEffect")]

    public sealed class SpellEffectEntry
    {
        public uint ID;
        public uint Effect;
        public int EffectBasePoints;
        public int EffectIndex;
        public int EffectAura;
        public int DifficultyID;
        public float EffectAmplitude;
        public int EffectAuraPeriod;
        public float EffectBonusCoefficient;
        public float EffectChainAmplitude;
        public int EffectChainTargets;
        public int EffectDieSides;
        public int EffectItemType;
        public int EffectMechanic;
        public float EffectPointsPerResource;
        public float EffectRealPointsPerLevel;
        public int EffectTriggerSpell;
        public float EffectPosFacing;
        public int EffectAttributes;
        public float BonusCoefficientFromAP;
        public float PvpMultiplier;
        public float Coefficient;
        public float Variance;
        public float ResourceCoefficient;
        public float GroupSizeBasePointsCoefficient;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] EffectSpellClassMask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] EffectMiscValue;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] EffectRadiusIndex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] ImplicitTarget;
        public int SpellID;
    }
}
