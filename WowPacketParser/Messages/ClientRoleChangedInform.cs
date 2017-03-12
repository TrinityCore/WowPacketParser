using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRoleChangedInform
    {
        public ulong ChangedUnit;
        public ulong From;
        public uint NewRole;
        public uint OldRole;
        public byte PartyIndex;
    }
}
