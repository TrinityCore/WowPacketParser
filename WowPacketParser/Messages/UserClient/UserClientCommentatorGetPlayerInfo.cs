using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCommentatorGetPlayerInfo
    {
        public ServerSpec WorldServer;
        public uint MapID;
    }
}
