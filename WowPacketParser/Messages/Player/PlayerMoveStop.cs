using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveStop
    {
        public CliMovementStatus Status;
    }
}
