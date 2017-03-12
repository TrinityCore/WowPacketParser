using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientWeeklySpellUsage
    {
        public List<WeeklySpellUse> Usage;
    }
}
