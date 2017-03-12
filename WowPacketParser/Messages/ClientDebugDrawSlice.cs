using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawSlice
    {
        public Vector3 Direction;
        public Vector3 Center;
        public float EndAngle;
        public uint Color;
        public float Outterradius;
        public float Innerradius;
        public float StartAngle;
        public float Lifetime;
        public uint Id;
        public Vector3 Normal;
        public uint Settings;
    }
}
