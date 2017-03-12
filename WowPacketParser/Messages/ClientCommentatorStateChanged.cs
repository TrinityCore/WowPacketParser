using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCommentatorStateChanged
    {
        public bool Enable;
        public ulong Guid;
    }
}
