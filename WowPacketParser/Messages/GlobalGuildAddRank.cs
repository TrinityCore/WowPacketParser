using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildAddRank
    {
        public string Name;
        public int RankOrder;
    }
}
