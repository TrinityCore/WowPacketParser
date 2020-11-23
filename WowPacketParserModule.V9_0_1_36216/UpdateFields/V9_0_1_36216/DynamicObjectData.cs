using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class DynamicObjectData : IDynamicObjectData
    {
        public WowGuid Caster { get; set; }
        public ISpellCastVisual SpellVisual { get; set; }
        public int SpellID { get; set; }
        public float Radius { get; set; }
        public uint CastTime { get; set; }
        public byte Type { get; set; }
    }
}

