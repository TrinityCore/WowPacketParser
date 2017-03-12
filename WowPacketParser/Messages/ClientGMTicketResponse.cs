using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMTicketResponse
    {
        public uint ResponseID;
        public string Description;
        public uint TicketID;
        public string ResponseText;
    }
}
