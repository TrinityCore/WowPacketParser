using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawLine
    {
        public float Thickness;
        public Vector3 A;
        public Vector3 B;
        public uint Colora;
        public uint Colorb;
        public uint Id;
        public uint Settings;
        public float Lifetime;
    }
}
