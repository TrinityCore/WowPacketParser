using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveStartDescend
    {
        public CliMovementStatus Status;
    }
}
