using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliBattlemasterJoinArenaSkirmish
    {
        public byte Roles;
        public byte Bracket;
        public bool JoinAsGroup;
    }
}
