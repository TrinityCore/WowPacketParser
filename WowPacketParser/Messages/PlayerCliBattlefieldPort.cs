using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliBattlefieldPort
    {
        public CliRideTicket Ticket;
        public bool AcceptedInvite;
    }
}
