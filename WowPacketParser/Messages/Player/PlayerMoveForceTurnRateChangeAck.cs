using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceTurnRateChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
