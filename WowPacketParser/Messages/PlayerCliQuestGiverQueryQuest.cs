using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQuestGiverQueryQuest
    {
        public int QuestID;
        public ulong QuestGiverGUID;
        public bool RespondToGiver;
    }
}
