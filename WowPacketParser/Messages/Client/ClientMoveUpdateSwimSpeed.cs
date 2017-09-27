using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateSwimSpeed
    {
        public CliMovementStatus Status;
        public float Speed;
    }
}
