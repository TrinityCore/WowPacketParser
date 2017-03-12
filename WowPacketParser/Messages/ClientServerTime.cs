using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientServerTime
    {
        public uint LastTick;
        public uint GameTime;
    }
}
