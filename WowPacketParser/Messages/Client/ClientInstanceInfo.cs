using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientInstanceInfo
    {
        public List<ClientInstanceLock> Locks;
    }
}
