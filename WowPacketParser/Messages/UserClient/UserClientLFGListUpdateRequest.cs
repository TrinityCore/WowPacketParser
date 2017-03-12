using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientLFGListUpdateRequest
    {
        public LFGListJoinRequest Info;
        public CliRideTicket Ticket;
    }
}
