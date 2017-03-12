using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveCharacterCheatSuccess
    {
        public ulong CharacterGUID;
        public Vector3 Position;
        public int MapID;
    }
}
