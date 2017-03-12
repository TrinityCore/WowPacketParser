using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateApplyMovementForce
    {
        public CliMovementStatus Status;
        public CliMovementForce Force;
    }
}
