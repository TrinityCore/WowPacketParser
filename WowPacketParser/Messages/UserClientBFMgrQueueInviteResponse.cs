using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBFMgrQueueInviteResponse
    {
        public ulong QueueID;
        public bool AcceptedInvite;
    }
}
