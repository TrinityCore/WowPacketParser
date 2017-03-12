using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveStopTurn
    {
        public CliMovementStatus Status;
    }
}
