using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateCollisionHeight
    {
        public float Scale;
        public CliMovementStatus Status;
        public float Height;
    }
}
