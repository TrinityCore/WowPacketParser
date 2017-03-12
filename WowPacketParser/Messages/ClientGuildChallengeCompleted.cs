using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGuildChallengeCompleted
    {
        public int MaxCount;
        public int ChallengeType;
        public int GoldAwarded;
        public int XpAwarded;
        public int CurrentCount;
    }
}
