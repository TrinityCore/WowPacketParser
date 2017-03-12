using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveSetCollisionHeightAck
    {
        public CliMovementAck Data;
        public UpdateCollisionHeightReason Reason;
        public uint MountDisplayID;
        public float Height;
    }
}
