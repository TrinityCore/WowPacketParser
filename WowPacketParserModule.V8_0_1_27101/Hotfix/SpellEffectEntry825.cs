using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_2_5_31921.Hotfix
{
    [HotfixStructure(DB2Hash.SpellEffect, ClientVersionBuild.V8_2_5_31921, HasIndexInData = false)]
    public class SpellEffectEntry
    {
        public short EffectAura { get; set; }
        public int DifficultyID { get; set; }
        public int EffectIndex { get; set; }
        public uint Effect { get; set; }
        public float EffectAmplitude { get; set; }
        public int EffectAttributes { get; set; }
        public int EffectAuraPeriod { get; set; }
        public float EffectBonusCoefficient { get; set; }
        public float EffectChainAmplitude { get; set; }
        public int EffectChainTargets { get; set; }
        public int EffectItemType { get; set; }
        public int EffectMechanic { get; set; }
        public float EffectPointsPerResource { get; set; }
        public float EffectPosFacing { get; set; }
        public float EffectRealPointsPerLevel { get; set; }
        public int EffectTriggerSpell { get; set; }
        public float BonusCoefficientFromAP { get; set; }
        public float PvpMultiplier { get; set; }
        public float Coefficient { get; set; }
        public float Variance { get; set; }
        public float ResourceCoefficient { get; set; }
        public float GroupSizeBasePointsCoefficient { get; set; }
        public float EffectBasePoints { get; set; }
        [HotfixArray(2)]
        public int[] EffectMiscValue { get; set; }
        [HotfixArray(2)]
        public uint[] EffectRadiusIndex { get; set; }
        [HotfixArray(4)]
        public int[] EffectSpellClassMask { get; set; }
        [HotfixArray(2)]
        public short[] ImplicitTarget { get; set; }
        public int SpellID { get; set; }
    }
}
