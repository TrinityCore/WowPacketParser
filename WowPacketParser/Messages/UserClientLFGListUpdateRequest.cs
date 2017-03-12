using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientLFGListUpdateRequest
    {
        public LFGListJoinRequest Info;
        public CliRideTicket Ticket;
    }
}
