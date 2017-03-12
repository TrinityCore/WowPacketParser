using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestUpdateAddPvPCredit
    {
        public int QuestID;
        public ushort Count;
    }
}
