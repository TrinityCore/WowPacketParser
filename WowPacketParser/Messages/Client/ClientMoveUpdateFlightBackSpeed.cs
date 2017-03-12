using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateFlightBackSpeed
    {
        public float Speed;
        public CliMovementStatus Status;
    }
}
