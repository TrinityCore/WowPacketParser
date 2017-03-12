using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerTimeSyncResponse
    {
        public uint ClientTime;
        public uint SequenceIndex;
    }
}
