using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInstanceEncounterEngageUnit
    {
        public ulong Unit;
        public byte TargetFramePriority;
    }
}
