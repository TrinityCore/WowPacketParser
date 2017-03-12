using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct QuestCompletionNPC
    {
        public int QuestID;
        public List<int> Npc;
    }
}
