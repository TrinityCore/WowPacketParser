using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliPosition
    {
        public Vector3 Position;
        public float Facing;
    }
}
