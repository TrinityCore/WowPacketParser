using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveRemoveMovementForceAck
    {
        public CliMovementAck Data;
        public uint MovementForceID;
    }
}
