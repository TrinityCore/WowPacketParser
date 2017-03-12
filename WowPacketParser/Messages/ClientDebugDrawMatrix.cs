using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawMatrix
    {
        public float Lifetime;
        public V4 Matrix00;
        public uint Colory;
        public V4 Matrix30;
        public uint Id;
        public uint Colorz;
        public V4 Matrix20;
        public uint Settings;
        public V4 Matrix10;
        public float Axisscale;
        public uint Colorx;
    }
}
