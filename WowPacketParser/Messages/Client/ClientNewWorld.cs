using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientNewWorld
    {
        public int MapID;
        public uint Reason;
        public float Facing;
        public Vector3 Position;
    }
}
