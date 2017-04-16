using System.Runtime.InteropServices;
namespace WowPacketParser.DBC.Structures
{
    [DBFile("SpellEffect")]

    public sealed class SpellEffectEntry
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public uint[] EffectSpellClassMask;
        public uint ID;
        public uint SpellID;
        public uint Effect;
        public uint EffectAura;
        public int EffectBasePoints;
        public uint EffectIndex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] EffectMiscValue;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] EffectRadiusIndex;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public uint[] ImplicitTarget;
        public uint DifficultyID;
        public float EffectAmplitude;
        public uint EffectAuraPeriod;
        public float EffectBonusCoefficient;
        public float EffectChainAmplitude;
        public uint EffectChainTargets;
        public int EffectDieSides;
        public uint EffectItemType;
        public uint EffectMechanic;
        public float EffectPointsPerResource;
        public float EffectRealPointsPerLevel;
        public uint EffectTriggerSpell;
        public float EffectPosFacing;
        public uint EffectAttributes;
        public float BonusCoefficientFromAP;
        public float PvPMultiplier;
    }
}
