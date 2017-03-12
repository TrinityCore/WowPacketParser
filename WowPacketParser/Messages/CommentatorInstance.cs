using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CommentatorInstance
    {
        public uint MapID;
        public ServerSpec WorldServer;
        public ulong InstanceID;
        public uint Status;
        public CommentatorTeam[/*2*/] Teams;
    }
}
