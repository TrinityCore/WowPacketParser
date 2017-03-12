using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQuestGiverCompleteQuest
    {
        public bool FromScript;
        public int QuestID;
        public ulong QuestGiverGUID;
    }
}
