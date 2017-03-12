using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMinimapPing
    {
        public ulong Sender;
        public Vector2 Position;
    }
}
