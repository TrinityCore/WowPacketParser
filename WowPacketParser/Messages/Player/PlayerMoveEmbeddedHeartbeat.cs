using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveEmbeddedHeartbeat
    {
        public CliMovementStatus Status;
    }
}
