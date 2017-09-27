using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForceTurnRateChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
