using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IConversationActor
    {
        int Id { get; }
        uint CreatureID { get; }
        uint CreatureDisplayInfoID { get; }
        WowGuid ActorGUID { get; }
        uint Type { get; }
        uint NoActorObject { get; }
    }
}
