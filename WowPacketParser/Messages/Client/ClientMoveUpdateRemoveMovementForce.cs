using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateRemoveMovementForce
    {
        public uint MovementForceID;
        public CliMovementStatus Status;
    }
}
