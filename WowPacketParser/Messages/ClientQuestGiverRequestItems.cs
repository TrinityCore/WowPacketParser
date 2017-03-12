using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestGiverRequestItems
    {
        public string QuestTitle;
        public QuestGiverRequestItems QuestData;
        public string CompletionText;
    }
}
