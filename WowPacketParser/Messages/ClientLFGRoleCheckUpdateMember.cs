using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGRoleCheckUpdateMember
    {
        public ulong Guid;
        public bool RoleCheckComplete;
        public uint RolesDesired;
        public byte Level;
    }
}
