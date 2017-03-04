using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

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

        public override void LoadValuesFromUpdateFields()
        {
            spellId             = UpdateFields.GetValue<AreaTriggerField, uint>(AreaTriggerField.AREATRIGGER_SPELLID);
            DecalPropertiesId   = UpdateFields.GetValue<AreaTriggerField, uint>(AreaTriggerField.AREATRIGGER_DECAL_PROPERTIES_ID);
            TimeToTarget        = UpdateFields.GetValue<AreaTriggerField, uint>(AreaTriggerField.AREATRIGGER_TIME_TO_TARGET);
            TimeToTargetScale   = UpdateFields.GetValue<AreaTriggerField, uint>(AreaTriggerField.AREATRIGGER_TIME_TO_TARGET_SCALE);
        }
    }
}
