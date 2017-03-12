using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawSetSourceColor
    {
        public uint Color;
        public uint Id;
        public uint SourceID;
    }
}
