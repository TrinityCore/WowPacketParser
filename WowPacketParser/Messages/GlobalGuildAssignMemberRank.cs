using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildAssignMemberRank
    {
        public ulong Member;
        public int RankOrder;
    }
}
