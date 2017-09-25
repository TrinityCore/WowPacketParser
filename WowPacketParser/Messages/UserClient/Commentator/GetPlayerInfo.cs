using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient.Commentator
{
    public unsafe struct GetPlayerInfo
    {
        public ServerSpec WorldServer;
        public uint MapID;
    }
}
