using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Player
{
    public unsafe struct PlayerMoveSetRelativePosition
    {
        public Vector3 Position;
        public float Facing;
    }
}
