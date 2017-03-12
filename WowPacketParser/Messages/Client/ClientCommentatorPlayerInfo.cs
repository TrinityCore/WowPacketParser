using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientCommentatorPlayerInfo
    {
        public uint MapID;
        public List<CommentatorPlayerInfo> PlayerInfo;
        public ulong InstanceID;
        public ServerSpec WorldServer;
    }
}
