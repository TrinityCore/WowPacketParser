using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQuestPushResult
    {
        public ulong TargetGUID;
        public int QuestID;
        public byte Result;
    }
}
