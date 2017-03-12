using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateRunBackSpeed
    {
        public float Speed;
        public CliMovementStatus Status;
    }
}
