using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMTicketGetTicketResponse
    {
        public ClientGMTicketInfo? Info; // Optional
        public int Result;
    }
}
