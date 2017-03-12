using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientQuestGiverRequestItems
    {
        public string QuestTitle;
        public QuestGiverRequestItems QuestData;
        public string CompletionText;
    }
}
