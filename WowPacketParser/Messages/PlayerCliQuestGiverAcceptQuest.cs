using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQuestGiverAcceptQuest
    {
        public int QuestID;
        public ulong QuestGiverGUID;
        public bool StartCheat;
    }
}
