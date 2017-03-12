using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct QuestCompletionNPC
    {
        public int QuestID;
        public List<int> Npc;
    }
}
