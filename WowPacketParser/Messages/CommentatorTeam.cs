using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CommentatorTeam
    {
        public ulong Guid;
        public List<CommentatorPlayer> Players;
    }
}
