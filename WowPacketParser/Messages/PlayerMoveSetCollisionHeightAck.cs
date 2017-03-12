using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveSetCollisionHeightAck
    {
        public CliMovementAck Data;
        public UpdateCollisionHeightReason Reason;
        public uint MountDisplayID;
        public float Height;
    }
}
