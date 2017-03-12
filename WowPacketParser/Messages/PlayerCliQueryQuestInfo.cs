using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQueryQuestInfo
    {
        public ulong QuestGiver;
        public uint QuestID;
    }
}
