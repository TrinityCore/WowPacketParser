using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveEmbeddedHeartbeat
    {
        public CliMovementStatus Status;
    }
}
