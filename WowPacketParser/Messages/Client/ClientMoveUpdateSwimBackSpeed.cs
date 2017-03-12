using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateSwimBackSpeed
    {
        public CliMovementStatus Status;
        public float Speed;
    }
}
