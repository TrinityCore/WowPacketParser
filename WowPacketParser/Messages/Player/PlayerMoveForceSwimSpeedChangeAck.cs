using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceSwimSpeedChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
