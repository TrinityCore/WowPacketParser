using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMUpdateTicketStatus
    {
        public ulong TargetGUID;
        public int TicketID;
        public int StatusInt;
    }
}
