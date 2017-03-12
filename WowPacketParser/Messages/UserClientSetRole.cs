using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetRole
    {
        public ulong ChangedUnit;
        public uint Role;
        public byte PartyIndex;
    }
}
