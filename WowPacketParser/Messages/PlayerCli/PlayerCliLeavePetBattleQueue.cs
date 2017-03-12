using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliLeavePetBattleQueue
    {
        public CliRideTicket Ticket;
    }
}
