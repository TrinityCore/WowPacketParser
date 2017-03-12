using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetSpellAutocast
    {
        public ulong PetGUID;
        public bool AutocastEnabled;
        public int SpellID;
    }
}
