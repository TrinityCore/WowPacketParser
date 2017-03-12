using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGListUpdateStatus
    {
        public bool Listed;
        public LFGListJoinRequest Request;
        public byte Reason;
        public CliRideTicket Ticket;
    }
}
