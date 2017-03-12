using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawSetGraphFrame
    {
        public uint Height;
        public uint Width;
        public uint Id;
    }
}
