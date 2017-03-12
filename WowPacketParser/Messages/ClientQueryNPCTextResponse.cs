using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryNPCTextResponse
    {
        public uint TextID;
        public bool Allow;
        public Data NpcText;
    }
}
