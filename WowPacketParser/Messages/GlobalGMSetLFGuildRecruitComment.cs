using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct GlobalGMSetLFGuildRecruitComment
    {
        public ulong PlayerGUID;
        public string Comment;
        public ulong GuildGUID;
    }
}
