using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class ObjectData : IObjectData
    {
        public int EntryID { get; set; }
        public uint DynamicFlags { get; set; }
        public float Scale { get; set; }
    }
}

