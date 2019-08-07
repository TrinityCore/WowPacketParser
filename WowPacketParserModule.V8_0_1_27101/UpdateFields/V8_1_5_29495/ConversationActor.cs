using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29495
{
    public class ConversationActor : IConversationActor
    {
        public uint Id { get; set; }
        public uint CreatureID { get; set; }
        public uint CreatureDisplayInfoID { get; set; }
        public WowGuid ActorGUID { get; set; }
        public int Field_18 { get; set; }
        public uint Type { get; set; }
    }
}

