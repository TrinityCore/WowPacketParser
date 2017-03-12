using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientTaskProgress
    {
        public uint TaskID;
        public uint FailureTime;
        public uint Flags;
        public List<ushort> Counts;
    }
}
