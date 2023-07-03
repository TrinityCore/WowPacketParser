using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_3_0_33062
{
    public class ConversationLine : IConversationLine
    {
        public int ConversationLineID { get; set; }
        public uint StartTime { get; set; }
        public int UiCameraID { get; set; }
        public byte ActorIndex { get; set; }
        public byte Flags { get; set; }
        public byte ChatType { get; set; }
    }
}

