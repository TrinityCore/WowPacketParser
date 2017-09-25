using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient.DF
{
    public unsafe struct SetComment
    {
        public CliRideTicket Ticket;
        public string Comment;
    }
}
