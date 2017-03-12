using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawBox
    {
        public uint Id;
        public uint Color;
        public float Lifetime;
        public Vector3 Extents;
        public V4 Matrix30;
        public uint Settings;
        public V4 Matrix10;
        public V4 Matrix20;
        public V4 Matrix00;
    }
}
