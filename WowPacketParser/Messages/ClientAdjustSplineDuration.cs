using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAdjustSplineDuration
    {
        public ulong Unit;
        public float Scale;
    }
}
