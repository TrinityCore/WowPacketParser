using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateRunBackSpeed
    {
        public float Speed;
        public CliMovementStatus Status;
    }
}
