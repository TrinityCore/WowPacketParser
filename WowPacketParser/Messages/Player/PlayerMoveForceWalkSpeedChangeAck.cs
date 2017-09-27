using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceWalkSpeedChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
