using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForcePitchRateChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
