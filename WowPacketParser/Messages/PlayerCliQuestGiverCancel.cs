using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQuestGiverCancel
    {
        public ulong QuestGiverGUID;
        public int QuestID;
    }
}
