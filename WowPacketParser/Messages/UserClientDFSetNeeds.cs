using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientDFSetNeeds
    {
        public bool Delta;
        public CliRideTicket Ticket;
        public fixed uint Needs[3];
    }
}
