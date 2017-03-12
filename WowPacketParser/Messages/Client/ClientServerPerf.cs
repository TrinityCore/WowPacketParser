using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientServerPerf
    {
        public List<ClientServerPerfStat> Stats;
    }
}
