using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRoleChosen
    {
        public bool Accepted;
        public uint RoleMask;
        public ulong Player;
    }
}
