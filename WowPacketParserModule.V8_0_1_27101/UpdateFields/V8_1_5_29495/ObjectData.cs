using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29495
{
    public class ObjectData : IObjectData
    {
        public int EntryID { get; set; }
        public uint DynamicFlags { get; set; }
        public float Scale { get; set; }
    }
}

