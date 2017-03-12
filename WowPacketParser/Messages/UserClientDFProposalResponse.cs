using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientDFProposalResponse
    {
        public uint ProposalID;
        public CliRideTicket Ticket;
        public bool Accepted;
        public ulong InstanceID;
    }
}
