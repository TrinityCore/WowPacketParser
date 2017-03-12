using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQuestGiverAutoLaunch
    {
        public ulong QuestGiverGUID;
        public int QuestID;
    }
}
