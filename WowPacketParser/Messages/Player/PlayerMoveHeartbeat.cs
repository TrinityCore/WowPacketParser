using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveHeartbeat
    {
        public CliMovementStatus Status;
    }
}
