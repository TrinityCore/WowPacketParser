using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveHeartbeat
    {
        public CliMovementStatus Status;
    }
}
