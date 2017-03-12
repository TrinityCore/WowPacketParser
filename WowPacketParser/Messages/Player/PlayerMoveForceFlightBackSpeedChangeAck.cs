using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceFlightBackSpeedChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
