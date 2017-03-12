using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateWalkSpeed
    {
        public CliMovementStatus Status;
        public float Speed;
    }
}
