using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CommentatorPlayer
    {
        public ulong Guid;
        public ServerSpec UserServer;
    }
}
