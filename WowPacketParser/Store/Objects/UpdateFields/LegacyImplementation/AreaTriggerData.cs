using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation
{
    public class AreaTriggerData : IAreaTriggerData
    {
        private WoWObject Object { get; }
        private Dictionary<int, UpdateField> UpdateFields => Object.UpdateFields;

        public AreaTriggerData(WoWObject obj)
        {
            Object = obj;
        }

        public int? SpellID => UpdateFields.GetValue<AreaTriggerField, int?>(AreaTriggerField.AREATRIGGER_SPELLID);
        public int? SpellForVisuals => UpdateFields.GetValue<AreaTriggerField, int?>(AreaTriggerField.AREATRIGGER_SPELL_FOR_VISUALS);
        public uint? TimeToTarget => UpdateFields.GetValue<AreaTriggerField, uint?>(AreaTriggerField.AREATRIGGER_TIME_TO_TARGET);
        public uint? TimeToTargetScale => UpdateFields.GetValue<AreaTriggerField, uint?>(AreaTriggerField.AREATRIGGER_TIME_TO_TARGET_SCALE);
        public uint? DecalPropertiesID => UpdateFields.GetValue<AreaTriggerField, uint?>(AreaTriggerField.AREATRIGGER_DECAL_PROPERTIES_ID);
    }
}