using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGListJoinResult
    {
        public CliRideTicket Ticket;
        public byte ResultDetail;
        public byte Result;
    }
}
