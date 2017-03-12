using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateWalkSpeed
    {
        public CliMovementStatus Status;
        public float Speed;
    }
}
