using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliPartyPlayerInfo
    {
        public string Name;
        public ulong Guid;
        public byte Connected;
        public byte Subgroup;
        public byte Flags;
        public byte RolesAssigned;
    }
}
