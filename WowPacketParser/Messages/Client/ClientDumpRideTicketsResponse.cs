using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDumpRideTicketsResponse
    {
        public List<ClientDumpRideTicket> Ticket;
    }
}
