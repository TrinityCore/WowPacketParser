using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveUpdateTurnRate
    {
        public float Speed;
        public CliMovementStatus Status;
    }
}
