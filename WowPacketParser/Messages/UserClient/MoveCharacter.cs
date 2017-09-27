using WowPacketParser.Misc;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct MoveCharacter
    {
        public ulong CharacterGUID;
        public int MapID;
        public Vector3 Position;
        public float Facing;
    }
}
