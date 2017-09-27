using WowPacketParser.Messages.CliChat;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveForcePitchRateChangeAck
    {
        public CliMovementSpeedAck Data;
    }
}
