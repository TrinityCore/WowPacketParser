using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateCollisionHeight
    {
        public float Scale;
        public CliMovementStatus Status;
        public float Height;
    }
}
