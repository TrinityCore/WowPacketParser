using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTest128BitGuidsResponse
    {
        public int128 Guid;
    }
}
