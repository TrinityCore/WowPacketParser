using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetBattleFinalRound
    {
        public bool Abandoned;
        public bool PvpBattle;
        public List<PetBattleFinalPet> Pets;
        public fixed bool Winners[2];
        public fixed int NpcCreatureID[2];
    }
}
