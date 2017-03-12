using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestUpdateAddCreditSimple
    {
        public int QuestID;
        public int ObjectID;
        public byte ObjectiveType;
    }
}
