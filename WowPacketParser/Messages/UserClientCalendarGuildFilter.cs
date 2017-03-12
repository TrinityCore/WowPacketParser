using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCalendarGuildFilter
    {
        public byte MinLevel;
        public byte MaxLevel;
        public byte MaxRankOrder;
    }
}
