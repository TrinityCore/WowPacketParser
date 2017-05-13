using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.SpellEffect, ClientVersionBuild.V7_2_0_23826)]
    public class SpellEffectEntry
    {
        [HotfixArray(4)]
        public uint[] EffectSpellClassMask { get; set; }
        public uint ID { get; set; }
        public uint SpellID { get; set; }
        public uint Effect { get; set; }
        public uint EffectAura { get; set; }
        public int EffectBasePoints { get; set; }
        public uint EffectIndex { get; set; }
        public int EffectMiscValue { get; set; }
        public int EffectMiscValueB { get; set; }
        public uint EffectRadiusIndex { get; set; }
        public uint EffectRadiusMaxIndex { get; set; }
        [HotfixArray(2)]
        public uint[] ImplicitTarget { get; set; }
        public uint DifficultyID { get; set; }
        public float EffectAmplitude { get; set; }
        public uint EffectAuraPeriod { get; set; }
        public float EffectBonusCoefficient { get; set; }
        public float EffectChainAmplitude { get; set; }
        public uint EffectChainTargets { get; set; }
        public int EffectDieSides { get; set; }
        public uint EffectItemType { get; set; }
        public uint EffectMechanic { get; set; }
        public float EffectPointsPerResource { get; set; }
        public float EffectRealPointsPerLevel { get; set; }
        public uint EffectTriggerSpell { get; set; }
        public float EffectPosFacing { get; set; }
        public uint EffectAttributes { get; set; }
        public float BonusCoefficientFromAP { get; set; }
        public float PvPMultiplier { get; set; }
    }
}
