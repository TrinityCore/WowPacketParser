using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInvalidatePlayer
    {
        public ulong Guid;
    }
}
