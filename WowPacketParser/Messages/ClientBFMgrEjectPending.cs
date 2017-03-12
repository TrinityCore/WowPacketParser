using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBFMgrEjectPending
    {
        public ulong QueueID;
        public bool Remote;
    }
}
