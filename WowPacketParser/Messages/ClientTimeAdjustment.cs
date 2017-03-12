using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTimeAdjustment
    {
        public uint SequenceIndex;
        public float TimeScale;
    }
}
