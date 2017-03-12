using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientClearQuestCompletedBit
    {
        public int QuestID;
        public int Bit;
    }
}
