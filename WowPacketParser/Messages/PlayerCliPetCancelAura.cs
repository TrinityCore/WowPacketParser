using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetCancelAura
    {
        public ulong PetGUID;
        public int SpellID;
    }
}
