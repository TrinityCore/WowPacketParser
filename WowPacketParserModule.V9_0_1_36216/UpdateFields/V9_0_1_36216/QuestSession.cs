using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class QuestSession : IQuestSession
    {
        public WowGuid Owner { get; set; }
        public ulong[] QuestCompleted { get; } = new ulong[875];
    }
}

