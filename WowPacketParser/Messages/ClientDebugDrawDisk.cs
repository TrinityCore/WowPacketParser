using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawDisk
    {
        public float Lifetime;
        public Vector3 Normal;
        public float Outterradius;
        public Vector3 Center;
        public uint Settings;
        public uint Color;
        public float Innerradius;
        public uint Id;
    }
}
