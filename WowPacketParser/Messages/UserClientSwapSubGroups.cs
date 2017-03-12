using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSwapSubGroups
    {
        public ulong Target1;
        public ulong Target2;
        public byte PartyIndex;
    }
}
