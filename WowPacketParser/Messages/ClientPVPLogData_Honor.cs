using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPVPLogData_Honor
    {
        public uint HonorKills;
        public uint Deaths;
        public uint ContributionPoints;
    }
}
