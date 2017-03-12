using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateRunSpeed
    {
        public CliMovementStatus Status;
        public float Speed;
    }
}
