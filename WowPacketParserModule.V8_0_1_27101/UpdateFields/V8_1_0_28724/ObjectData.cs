using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_0_28724
{
    public class ObjectData : IObjectData
    {
        public int EntryID { get; set; }
        public uint DynamicFlags { get; set; }
        public float Scale { get; set; }
    }
}

