using WowPacketParser.Messages.Cli;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveApplyMovementForceAck
    {
        public int TransportID;
        public Vector2 Direction;
        public uint MovementForceID;
        public CliMovementAck Data;
    }
}
