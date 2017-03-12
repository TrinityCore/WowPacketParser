using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientConsoleWrite
    {
        public uint Color;
        public string Line;
    }
}
