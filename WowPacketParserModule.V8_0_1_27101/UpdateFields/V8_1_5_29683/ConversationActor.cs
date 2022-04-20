using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V8_0_1_27101.UpdateFields.V8_1_5_29683
{
    public class ConversationActor : IConversationActor
    {
        public uint CreatureID { get; set; }
        public uint CreatureDisplayInfoID { get; set; }
        public WowGuid ActorGUID { get; set; }
        public int Id { get; set; }
        public uint Type { get; set; }
        public uint NoActorObject { get; set; }
    }
}

