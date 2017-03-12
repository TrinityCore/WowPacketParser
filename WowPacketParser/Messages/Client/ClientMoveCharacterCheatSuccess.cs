using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveCharacterCheatSuccess
    {
        public ulong CharacterGUID;
        public Vector3 Position;
        public int MapID;
    }
}
