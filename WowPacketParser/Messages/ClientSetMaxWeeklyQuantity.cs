using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetMaxWeeklyQuantity
    {
        public int MaxWeeklyQuantity;
        public int Type;
    }
}
