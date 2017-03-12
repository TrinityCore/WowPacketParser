using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawSphere
    {
        public float Lifetime;
        public uint Settings;
        public float Radius;
        public Vector3 Center;
        public uint Id;
        public uint Color;
    }
}
