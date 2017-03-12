using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCommentatorPlayerInfo
    {
        public uint MapID;
        public List<CommentatorPlayerInfo> PlayerInfo;
        public ulong InstanceID;
        public ServerSpec WorldServer;
    }
}
