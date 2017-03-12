using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliBattlefieldPort
    {
        public CliRideTicket Ticket;
        public bool AcceptedInvite;
    }
}
