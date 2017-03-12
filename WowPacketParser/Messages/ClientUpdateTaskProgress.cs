using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUpdateTaskProgress
    {
        public List<ClientTaskProgress> Progress;
    }
}
