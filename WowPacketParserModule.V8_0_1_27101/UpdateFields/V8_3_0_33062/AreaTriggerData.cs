using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_3_0_33062
{
    public class AreaTriggerData : IAreaTriggerData
    {
        public IScaleCurve OverrideScaleCurve { get; set; }
        public IScaleCurve ExtraScaleCurve { get; set; }
        public WowGuid Caster { get; set; }
        public uint Duration { get; set; }
        public uint? TimeToTarget { get; set; }
        public uint? TimeToTargetScale { get; set; }
        public uint TimeToTargetExtraScale { get; set; }
        public int? SpellID { get; set; }
        public int? SpellForVisuals { get; set; }
        public int SpellXSpellVisualID { get; set; }
        public float BoundsRadius2D { get; set; }
        public uint? DecalPropertiesID { get; set; }
        public WowGuid CreatingEffectGUID { get; set; }
    }
}

