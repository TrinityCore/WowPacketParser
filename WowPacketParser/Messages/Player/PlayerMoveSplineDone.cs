using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveSplineDone
    {
        public uint SplineID;
        public CliMovementStatus Status;
    }
}
