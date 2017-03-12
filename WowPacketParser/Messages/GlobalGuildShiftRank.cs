using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGuildShiftRank
    {
        public bool ShiftUp;
        public int RankOrder;
    }
}
