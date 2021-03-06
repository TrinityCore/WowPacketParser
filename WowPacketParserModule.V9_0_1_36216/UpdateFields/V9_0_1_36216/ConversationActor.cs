using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
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

