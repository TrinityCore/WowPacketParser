using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceSwimBackSpeedChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
