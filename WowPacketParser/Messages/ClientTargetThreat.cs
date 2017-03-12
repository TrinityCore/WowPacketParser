using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTargetThreat
    {
        public ulong TargetGUID;
        public uint Threat;
    }
}
