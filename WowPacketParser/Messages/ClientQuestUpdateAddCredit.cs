using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQuestUpdateAddCredit
    {
        public ulong VictimGUID;
        public int ObjectID;
        public int QuestID;
        public ushort Count;
        public ushort Required;
        public byte ObjectiveType;
    }
}
