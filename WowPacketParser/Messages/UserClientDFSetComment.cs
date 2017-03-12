using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientDFSetComment
    {
        public CliRideTicket Ticket;
        public string Comment;
    }
}
