using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawQuad
    {
        public Vector2 Size;
        public float Lifetime;
        public uint Settings;
        public string Texture;
        public uint Id;
        public Vector2 TopLeft;
        public uint Flags;
        public Vector2 Uv_max;
        public uint Color;
        public Vector2 Uv_min;
    }
}
