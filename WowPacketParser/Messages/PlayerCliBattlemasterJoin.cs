using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliBattlemasterJoin
    {
        public bool JoinAsGroup;
        public byte Roles;
        public ulong QueueID;
        public fixed int BlacklistMap[2];
    }
}
