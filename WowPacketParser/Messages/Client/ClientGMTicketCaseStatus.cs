using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGMTicketCaseStatus
    {
        public UnixTime OldestTicketTime;
        public UnixTime UpdateTime;
        public List<GMTicketCase> Cases;
    }
}
