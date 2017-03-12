using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLoginVerifyWorld
    {
        public float Facing;
        public int MapID;
        public uint Reason;
        public Vector3 Position;
    }
}
