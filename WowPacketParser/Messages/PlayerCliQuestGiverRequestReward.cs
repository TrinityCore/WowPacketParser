using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQuestGiverRequestReward
    {
        public ulong QuestGiverGUID;
        public int QuestID;
    }
}
