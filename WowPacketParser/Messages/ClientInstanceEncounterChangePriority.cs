using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInstanceEncounterChangePriority
    {
        public ulong Unit;
        public byte TargetFramePriority;
    }
}
