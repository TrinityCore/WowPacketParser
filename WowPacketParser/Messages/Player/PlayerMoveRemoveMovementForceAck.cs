using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveRemoveMovementForceAck
    {
        public CliMovementAck Data;
        public uint MovementForceID;
    }
}
