using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGListJoinResult
    {
        public CliRideTicket Ticket;
        public byte ResultDetail;
        public byte Result;
    }
}
