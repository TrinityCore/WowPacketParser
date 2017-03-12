using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceWalkSpeedChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
