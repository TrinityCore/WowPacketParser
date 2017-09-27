using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateRemoveMovementForce
    {
        public uint MovementForceID;
        public CliMovementStatus Status;
    }
}
