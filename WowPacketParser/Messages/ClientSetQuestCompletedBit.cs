using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetQuestCompletedBit
    {
        public int Bit;
        public int QuestID;
    }
}
