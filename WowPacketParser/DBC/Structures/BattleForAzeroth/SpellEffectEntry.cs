﻿namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("SpellEffect")]

    public sealed class SpellEffectEntry
    {
        public uint ID;
        public short EffectAura;
        public int DifficultyID;
        public int EffectIndex;
        public uint Effect;
        public float EffectAmplitude;
        public int EffectAttributes;
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
        public int[] EffectMiscValue = new int[2];
        public uint[] EffectRadiusIndex = new uint[2];
        public int[] EffectSpellClassMask = new int[4];
        public short[] ImplicitTarget = new short[2];
        public int SpellID;
    }
}
