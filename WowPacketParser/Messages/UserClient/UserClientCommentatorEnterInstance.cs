using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCommentatorEnterInstance
    {
        public ulong InstanceID;
        public uint MapID;
        public int DifficultyID;
        public ServerSpec WorldServer;
    }
}
