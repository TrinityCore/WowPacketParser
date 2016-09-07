using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFileName("SpellEffect")]

    public sealed class SpellEffectEntry
    {
        public float EffectAmplitude;
        public float EffectBonusCoefficient;
        public float EffectChainAmplitude;
        public float EffectPointsPerResource;
        public float EffectRealPointsPerLevel;
        public uint[] EffectSpellClassMask;
        public float EffectPosFacing;
        public float BonusCoefficientFromAP;
        public uint ID;
        public uint DifficultyID;
        public uint Effect;
        public uint EffectAura;
        public uint EffectAuraPeriod;
        public uint EffectBasePoints;
        public uint EffectChainTargets;
        public uint EffectDieSides;
        public uint EffectItemType;
        public uint EffectMechanic;
        public int[] EffectMiscValue;
        public uint[] EffectRadiusIndex;
        public uint EffectTriggerSpell;
        public uint[] ImplicitTarget;
        public uint SpellID;
        public uint EffectIndex;
        public uint EffectAttributes;
    }
}
