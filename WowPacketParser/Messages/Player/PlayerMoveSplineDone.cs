using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveSplineDone
    {
        public uint SplineID;
        public CliMovementStatus Status;
    }
}
