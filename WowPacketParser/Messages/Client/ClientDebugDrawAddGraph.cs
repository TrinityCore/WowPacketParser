using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientDebugDrawAddGraph
    {
        public string Label;
        public Vector2 Bounds_min;
        public uint Height;
        public uint Flags;
        public uint DefaultSampleColor;
        public uint SampleCapacity;
        public uint Id;
        public Vector2 Bounds_max;
        public uint Width;
    }
}
