using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CollisionHeightInfo
    {
        public float Height;
        public float Scale;
        public UpdateCollisionHeightReason Reason;
    }
}
