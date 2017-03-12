using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceRunSpeedChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
