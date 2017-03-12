using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientAddBattlePet
    {
        public bool AllSpecies;
        public int SpeciesID;
        public int CreatureID;
        public bool IgnoreMaxPetRestriction;
    }
}
