using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientServerPerf
    {
        public List<ClientServerPerfStat> Stats;
    }
}
