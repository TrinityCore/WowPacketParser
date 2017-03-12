using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCommentatorMapInfo
    {
        public ulong PlayerInstanceID;
        public List<CommentatorMap> Maps;
    }
}
