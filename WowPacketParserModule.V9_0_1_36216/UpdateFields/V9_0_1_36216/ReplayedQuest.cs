using WowPacketParser.Store.Objects.UpdateFields;

namespace WowPacketParserModule.V9_0_1_36216.UpdateFields.V9_0_1_36216
{
    public class ReplayedQuest : IReplayedQuest
    {
        public int QuestID { get; set; }
        public uint ReplayTime { get; set; }
    }
}

