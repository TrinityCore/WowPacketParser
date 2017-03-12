using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveStartStrafeLeft
    {
        public CliMovementStatus Status;
    }
}
