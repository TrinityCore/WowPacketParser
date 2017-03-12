using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveEmbeddedHeartbeat
    {
        public CliMovementStatus Status;
    }
}
