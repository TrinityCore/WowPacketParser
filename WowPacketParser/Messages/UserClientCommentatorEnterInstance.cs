using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientCommentatorEnterInstance
    {
        public ulong InstanceID;
        public uint MapID;
        public int DifficultyID;
        public ServerSpec WorldServer;
    }
}
