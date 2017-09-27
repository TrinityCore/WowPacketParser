using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceFlightSpeedChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
