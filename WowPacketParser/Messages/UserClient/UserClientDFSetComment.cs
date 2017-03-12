using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientDFSetComment
    {
        public CliRideTicket Ticket;
        public string Comment;
    }
}
