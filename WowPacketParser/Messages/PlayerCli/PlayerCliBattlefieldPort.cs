using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliBattlefieldPort
    {
        public CliRideTicket Ticket;
        public bool AcceptedInvite;
    }
}
