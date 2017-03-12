using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct QuestGiverStatus
    {
        public ulong Guid;
        public uint Status;
    }
}
