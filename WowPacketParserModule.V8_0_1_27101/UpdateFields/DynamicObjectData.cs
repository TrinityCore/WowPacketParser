using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29495
{
    public class DynamicObjectData : IDynamicObjectData
    {
        public WowGuid Caster { get; set; }
        public int SpellXSpellVisualID { get; set; }
        public int SpellID { get; set; }
        public float Radius { get; set; }
        public uint CastTime { get; set; }
        public byte Type { get; set; }
    }
}

