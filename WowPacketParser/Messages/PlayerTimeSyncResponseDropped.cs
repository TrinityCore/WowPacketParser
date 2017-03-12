using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerTimeSyncResponseDropped
    {
        public uint SequenceIndexFirst;
        public uint SequenceIndexLast;
    }
}
