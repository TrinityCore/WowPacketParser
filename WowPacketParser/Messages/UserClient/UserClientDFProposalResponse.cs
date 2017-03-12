using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientDFProposalResponse
    {
        public uint ProposalID;
        public CliRideTicket Ticket;
        public bool Accepted;
        public ulong InstanceID;
    }
}
