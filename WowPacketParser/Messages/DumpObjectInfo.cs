using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct DumpObjectInfo
    {
        public ulong Guid;
        public float VisibleRange;
        public Vector3 Position;
        public uint DisplayID;
        public bool Granted;
    }
}
