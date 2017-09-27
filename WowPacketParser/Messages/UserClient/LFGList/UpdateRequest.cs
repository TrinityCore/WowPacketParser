using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient.LFGList
{
    public unsafe struct UpdateRequest
    {
        public LFGListJoinRequest Info;
        public CliRideTicket Ticket;
    }
}
