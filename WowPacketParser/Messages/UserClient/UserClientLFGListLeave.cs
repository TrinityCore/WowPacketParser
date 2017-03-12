using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientLFGListLeave
    {
        public CliRideTicket Ticket;
    }
}
