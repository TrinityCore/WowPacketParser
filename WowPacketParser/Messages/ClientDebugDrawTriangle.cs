using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawTriangle
    {
        public uint Colorb;
        public uint Colora;
        public uint Settings;
        public Vector3 A;
        public Vector3 C;
        public uint Colorc;
        public Vector3 B;
        public uint Id;
        public float Lifetime;
    }
}
