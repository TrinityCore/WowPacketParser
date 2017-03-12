using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdatePitchRate
    {
        public float Speed;
        public CliMovementStatus Status;
    }
}
