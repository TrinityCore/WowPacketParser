using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientDFLeave
    {
        public CliRideTicket Ticket;
    }
}
