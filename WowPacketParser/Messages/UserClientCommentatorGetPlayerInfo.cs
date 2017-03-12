using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCommentatorGetPlayerInfo
    {
        public ServerSpec WorldServer;
        public uint MapID;
    }
}
