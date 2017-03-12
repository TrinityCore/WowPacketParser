using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawAddSample
    {
        public uint Id;
        public float X;
        public float Y;
        public uint SourceID;
    }
}
