using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCommentatorMapInfo
    {
        public ulong PlayerInstanceID;
        public List<CommentatorMap> Maps;
    }
}
