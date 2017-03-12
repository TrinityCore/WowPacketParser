using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliQueryPetName
    {
        public ulong PetID;
        public ulong UnitGUID;
    }
}
