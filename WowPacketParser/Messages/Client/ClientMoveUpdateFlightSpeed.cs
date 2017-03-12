using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateFlightSpeed
    {
        public CliMovementStatus Status;
        public float Speed;
    }
}
