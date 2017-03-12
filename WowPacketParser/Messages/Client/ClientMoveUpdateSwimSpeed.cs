using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateSwimSpeed
    {
        public CliMovementStatus Status;
        public float Speed;
    }
}
