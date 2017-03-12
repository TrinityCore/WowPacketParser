using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBFMgrEntering
    {
        public bool Relocated;
        public bool ClearedAFK;
        public bool OnOffense;
        public ulong QueueID;
    }
}
