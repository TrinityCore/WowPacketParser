using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient.DF
{
    public unsafe struct SetNeeds
    {
        public bool Delta;
        public CliRideTicket Ticket;
        public fixed uint Needs[3];
    }
}
