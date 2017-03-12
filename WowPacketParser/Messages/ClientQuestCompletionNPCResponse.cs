using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestCompletionNPCResponse
    {
        public List<QuestCompletionNPC> QuestCompletionNPCs;
    }
}
