using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTaskProgress
    {
        public uint TaskID;
        public uint FailureTime;
        public uint Flags;
        public List<ushort> Counts;
    }
}
