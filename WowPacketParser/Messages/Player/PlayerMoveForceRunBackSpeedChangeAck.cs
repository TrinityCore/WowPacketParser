using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceRunBackSpeedChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
