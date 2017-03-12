using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawText3D
    {
        public uint Settings;
        public string Text;
        public uint Id;
        public uint Color;
        public byte Alignment;
        public Vector3 Location;
        public float Height;
        public float Lifetime;
    }
}
