using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CommentatorTeam
    {
        public ulong Guid;
        public List<CommentatorPlayer> Players;
    }
}
