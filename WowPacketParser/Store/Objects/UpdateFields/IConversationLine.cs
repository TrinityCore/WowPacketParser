
namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IConversationLine
    {
        int ConversationLineID { get; }
        uint StartTime { get; }
        int UiCameraID { get; }
        byte ActorIndex { get; }
        byte Flags { get; }
        byte ChatType { get; }
    }
}
