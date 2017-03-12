using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct Location
    {
        public ulong Transport;
        public Vector3 Loc;
    }
}
