using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientDFSetNeeds
    {
        public bool Delta;
        public CliRideTicket Ticket;
        public fixed uint Needs[3];
    }
}
