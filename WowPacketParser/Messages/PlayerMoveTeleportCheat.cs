using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerMoveTeleportCheat
    {
        public float Facing;
        public Vector3 Position;
    }
}
