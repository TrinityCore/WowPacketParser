using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using WowPacketParser.Store.Objects.UpdateFields;
using WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_areatrigger")]
    public sealed class SpellAreaTrigger : WoWObject, IDataModel
    {
        [DBFieldName("SpellMiscId", true)]
        public uint? SpellMiscId;

        [DBFieldName("AreaTriggerId", true)]
        public uint? AreaTriggerId;

        [DBFieldName("MoveCurveId")]
        public int? MoveCurveId = 0;

        [DBFieldName("ScaleCurveId")]
        public int? ScaleCurveId = 0;

        [DBFieldName("MorphCurveId")]
        public int? MorphCurveId = 0;

        [DBFieldName("FacingCurveId")]
        public int? FacingCurveId = 0;

        [DBFieldName("AnimId")]
        public int? AnimId = 0;

        [DBFieldName("AnimKitId")]
        public int? AnimKitId = 0;

        [DBFieldName("DecalPropertiesId")]
        public uint DecalPropertiesId = 0;

        [DBFieldName("TimeToTarget")]
        public uint TimeToTarget = 0;

        [DBFieldName("TimeToTargetScale")]
        public uint TimeToTargetScale = 0;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        // Will be inserted as comment to facilitate SpellMiscId research
        public uint spellId = 0;

        public IAreaTriggerData AreaTriggerData;

        public SpellAreaTrigger() : base()
        {
            AreaTriggerData = new AreaTriggerData(this);
        }

        public override void LoadValuesFromUpdateFields()
        {
            spellId             = (uint)AreaTriggerData.SpellID;
            DecalPropertiesId   = AreaTriggerData.DecalPropertiesID;
            TimeToTarget        = AreaTriggerData.TimeToTarget;
            TimeToTargetScale   = AreaTriggerData.TimeToTargetScale;

            if (Settings.UseDBC)
            {
                for (uint idx = 0; idx < 32; idx++)
                {
                    var tuple = Tuple.Create(spellId, idx);
                    if (DBC.DBC.SpellEffectStores.ContainsKey(tuple))
                    {
                        var effect = DBC.DBC.SpellEffectStores[tuple];

                        if (effect.Effect == (uint)SpellEffects.SPELL_EFFECT_CREATE_AREATRIGGER ||
                            effect.EffectAura == (uint)AuraTypeLegion.SPELL_AURA_AREA_TRIGGER)
                        {
                            // If we already had a SPELL_EFFECT_CREATE_AREATRIGGER, spell has multiple areatrigger,
                            // so we can't deduce SpellMiscId, erase previous value & break
                            if (SpellMiscId != null)
                            {
                                SpellMiscId = null;
                                break;
                            }

                            SpellMiscId = (uint)effect.EffectMiscValue[0];
                        }
                    }
                }
            }
        }
    }
}
