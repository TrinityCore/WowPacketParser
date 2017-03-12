using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQuestGiverChooseReward
    {
        public ulong QuestGiverGUID;
        public int QuestID;
        public int ItemChoiceID;
    }
}
