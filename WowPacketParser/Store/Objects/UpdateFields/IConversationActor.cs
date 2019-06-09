using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IConversationActor
    {
        uint CreatureID { get; }
        uint CreatureDisplayInfoID { get; }
        WowGuid ActorGUID { get; }
        uint Type { get; }
    }
}
