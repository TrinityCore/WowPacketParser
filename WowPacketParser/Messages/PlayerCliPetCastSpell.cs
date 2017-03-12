using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetCastSpell
    {
        public ulong PetGUID;
        public SpellCastRequest Cast;
    }
}
