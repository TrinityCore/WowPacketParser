using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTreasureDebug
    {
        public uint Depth;
        public string Text;
    }
}
