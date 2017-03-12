using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGJoinResult
    {
        public byte Result;
        public List<ClientLFGBlackList> BlackList;
        public byte ResultDetail;
        public CliRideTicket Ticket;
    }
}
