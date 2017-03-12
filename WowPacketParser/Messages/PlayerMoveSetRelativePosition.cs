using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveSetRelativePosition
    {
        public Vector3 Position;
        public float Facing;
    }
}
