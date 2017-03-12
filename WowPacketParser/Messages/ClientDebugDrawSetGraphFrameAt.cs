using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawSetGraphFrameAt
    {
        public int Y;
        public uint Id;
        public int X;
        public uint Height;
        public uint Width;
    }
}
