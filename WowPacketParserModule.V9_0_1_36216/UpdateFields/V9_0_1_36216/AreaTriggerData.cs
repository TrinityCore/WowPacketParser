using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class AreaTriggerData : IAreaTriggerData
    {
        public WowGuid Caster { get; set; }
        public uint Duration { get; set; }
        public uint? TimeToTarget { get; set; }
        public uint? TimeToTargetScale { get; set; }
        public uint TimeToTargetExtraScale { get; set; }
        public int? SpellID { get; set; }
        public int SpellForVisuals { get; set; }
        public ISpellCastVisual SpellVisual { get; set; }
        public float BoundsRadius2D { get; set; }
        public uint? DecalPropertiesID { get; set; }
        public WowGuid CreatingEffectGUID { get; set; }
        public IScaleCurve OverrideScaleCurve { get; set; }
        public IScaleCurve ExtraScaleCurve { get; set; }
    }
}

