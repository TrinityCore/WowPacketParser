using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGListJoinResult
    {
        public CliRideTicket Ticket;
        public byte ResultDetail;
        public byte Result;
    }
}
