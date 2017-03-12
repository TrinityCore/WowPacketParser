using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGProposalUpdate
    {
        public bool ProposalSilent;
        public List<ClientLFGProposalUpdatePlayer> Players;
        public uint CompletedMask;
        public ulong InstanceID;
        public bool ValidCompletedMask;
        public uint Slot;
        public CliRideTicket Ticket;
        public sbyte State;
        public uint ProposalID;
    }
}
