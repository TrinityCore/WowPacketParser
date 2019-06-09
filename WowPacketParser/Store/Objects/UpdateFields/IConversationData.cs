using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IConversationData
    {
        IConversationLine[] Lines { get; }
        int LastLineEndTime { get; }
        DynamicUpdateField<IConversationActor> Actors { get; }
    }
}
