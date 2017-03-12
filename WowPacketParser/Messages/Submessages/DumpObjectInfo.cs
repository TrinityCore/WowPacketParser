using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
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
