using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateTurnRate
    {
        public float Speed;
        public CliMovementStatus Status;
    }
}
