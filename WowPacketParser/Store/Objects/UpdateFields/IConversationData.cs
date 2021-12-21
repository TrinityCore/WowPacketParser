using System;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields
{
    public interface IConversationData
    {
        IConversationLine[] Lines { get; }
        int? LastLineEndTime { get; }
        DynamicUpdateField<IConversationActor> Actors { get; }
    }

    public interface IMutableConversationData : IConversationData
    {
        new IConversationLine[] Lines { get; set; }
        new int? LastLineEndTime { get; set; }
    }

    public static partial class Extensions
    {
        public static void UpdateData(this IMutableConversationData data, IConversationData update)
        {
            data.LastLineEndTime = update.LastLineEndTime ?? data.LastLineEndTime;
            data.Lines = update.Lines ?? data.Lines;

            if (update.Actors.Count > data.Actors.Count)
                data.Actors.Resize((uint)update.Actors.Count);

            for (int i = 0; i < update.Actors.Count; ++i)
                data.Actors[i] = update.Actors[i] ?? data.Actors[i];
        }
    }
}
