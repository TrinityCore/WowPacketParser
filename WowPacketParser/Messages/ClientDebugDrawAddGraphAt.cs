using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawAddGraphAt
    {
        public string Label;
        public uint DefaultSampleColor;
        public Vector2 Bounds_min;
        public Vector2 Bounds_max;
        public uint Width;
        public int X;
        public uint Height;
        public int Y;
        public uint SampleCapacity;
        public uint Flags;
        public uint Id;
    }
}
