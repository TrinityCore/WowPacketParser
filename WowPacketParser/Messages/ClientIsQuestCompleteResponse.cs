using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientIsQuestCompleteResponse
    {
        public int QuestID;
        public bool Complete;
    }
}
