using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation
{
    public class ObjectData : IObjectData
    {
        private WoWObject Object { get; }
        private Dictionary<int, UpdateField> UpdateFields => Object.UpdateFields;

        public ObjectData(WoWObject obj)
        {
            Object = obj;
        }

        public int? EntryID => UpdateFields.GetValue<ObjectField, int>(ObjectField.OBJECT_FIELD_ENTRY);

        public uint? DynamicFlags => UpdateFields.GetValue<ObjectField, uint>(ObjectField.OBJECT_DYNAMIC_FLAGS);

        public float? Scale => UpdateFields.GetValue<ObjectField, float?>(ObjectField.OBJECT_FIELD_SCALE_X).GetValueOrDefault(1.0f);
    }
}
