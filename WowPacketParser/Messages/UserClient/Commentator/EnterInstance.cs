using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient.Commentator
{
    public unsafe struct EnterInstance
    {
        public ulong InstanceID;
        public uint MapID;
        public int DifficultyID;
        public ServerSpec WorldServer;
    }
}
