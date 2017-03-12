using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInstanceInfo
    {
        public List<ClientInstanceLock> Locks;
    }
}
