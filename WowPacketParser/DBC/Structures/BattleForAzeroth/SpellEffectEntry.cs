using System.Runtime.InteropServices;
namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("SpellEffect")]

    public sealed class SpellEffectEntry
    {
        public int DifficultyID;
        public int EffectIndex;
        public uint Effect;
        public float EffectAmplitude;
        public int EffectAttributes;
        public short EffectAura;
        public int EffectAuraPeriod;
        public float EffectBonusCoefficient;
        public float EffectChainAmplitude;
        public int EffectChainTargets;
        public int EffectItemType;
        public int EffectMechanic;
        public float EffectPointsPerResource;
        public float EffectPosFacing;
        public float EffectRealPointsPerLevel;
        public int EffectTriggerSpell;
        public float BonusCoefficientFromAP;
        public float PvpMultiplier;
        public float Coefficient;
        public float Variance;
        public float ResourceCoefficient;
        public float GroupSizeBasePointsCoefficient;
        public float EffectBasePoints;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] EffectMiscValue;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] EffectRadiusIndex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public int[] EffectSpellClassMask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public short[] ImplicitTarget;
        public int SpellID;
    }
}
