using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawArrow
    {
        public float Headscale;
        public uint Settings;
        public float Lifetime;
        public Vector3 Location;
        public uint Color;
        public Vector3 Direction;
        public uint Id;
        public float Length;
    }
}
