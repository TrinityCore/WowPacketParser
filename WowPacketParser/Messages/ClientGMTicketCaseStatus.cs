using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMTicketCaseStatus
    {
        public UnixTime OldestTicketTime;
        public UnixTime UpdateTime;
        public List<GMTicketCase> Cases;
    }
}
