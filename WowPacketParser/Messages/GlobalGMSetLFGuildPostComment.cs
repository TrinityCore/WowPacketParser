using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGMSetLFGuildPostComment
    {
        public ulong GuildGUID;
        public string Comment;
    }
}
