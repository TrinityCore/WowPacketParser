using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveSetCanTurnWhileFallingAck
    {
        public CliMovementAck Data;
    }
}
