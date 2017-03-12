using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSetCollisionHeight
    {
        public float Scale;
        public ulong MoverGUID;
        public uint MountDisplayID;
        public UpdateCollisionHeightReason Reason;
        public uint SequenceIndex;
        public float Height;
    }
}
