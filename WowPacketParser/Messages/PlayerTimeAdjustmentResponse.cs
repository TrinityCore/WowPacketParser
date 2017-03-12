using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerTimeAdjustmentResponse
    {
        public uint SequenceIndex;
        public uint ClientTime;
    }
}
