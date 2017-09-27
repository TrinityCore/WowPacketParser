using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceFlightBackSpeedChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
