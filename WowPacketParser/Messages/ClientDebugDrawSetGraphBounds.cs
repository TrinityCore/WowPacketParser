using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawSetGraphBounds
    {
        public Vector2 Bounds_max;
        public uint Id;
        public Vector2 Bounds_min;
    }
}
