using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceTurnRateChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
