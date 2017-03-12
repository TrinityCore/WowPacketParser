using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientDFSetNeeds
    {
        public bool Delta;
        public CliRideTicket Ticket;
        public fixed uint Needs[3];
    }
}
