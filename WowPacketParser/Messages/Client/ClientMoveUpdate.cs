using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdate
    {
        public CliMovementStatus Status;
    }
}
