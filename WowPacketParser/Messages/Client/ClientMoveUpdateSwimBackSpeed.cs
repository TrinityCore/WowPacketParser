using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateSwimBackSpeed
    {
        public CliMovementStatus Status;
        public float Speed;
    }
}
