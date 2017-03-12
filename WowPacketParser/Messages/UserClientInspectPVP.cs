using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientInspectPVP
    {
        public ulong InspectTarget;
        public uint InspectRealmAddress;
    }
}
