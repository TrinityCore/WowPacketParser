using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientDFSetComment
    {
        public CliRideTicket Ticket;
        public string Comment;
    }
}
