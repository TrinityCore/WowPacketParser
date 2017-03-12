using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBFMgrStateChanged
    {
        public ulong QueueID;
        public int State;
    }
}
