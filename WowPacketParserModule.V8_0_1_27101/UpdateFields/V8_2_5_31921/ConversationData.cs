using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_2_5_31921
{
    public class ConversationData : IMutableConversationData
    {
        public int? LastLineEndTime { get; set; }
        public uint Progress { get; set; }
        public IConversationLine[] Lines { get; set; }
        public DynamicUpdateField<IConversationActor> Actors { get; } = new DynamicUpdateField<IConversationActor>();
    }
}

