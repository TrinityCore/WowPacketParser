using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGListUpdateStatus
    {
        public bool Listed;
        public LFGListJoinRequest Request;
        public byte Reason;
        public CliRideTicket Ticket;
    }
}
