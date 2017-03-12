using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBFMgrEntryInvite
    {
        public int AreaID;
        public UnixTime ExpireTime;
        public ulong QueueID;
    }
}
