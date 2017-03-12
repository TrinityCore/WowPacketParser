using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetBattleRequestPVP
    {
        public PetBattleLocations Location;
        public ulong OpponentCharacterID;
    }
}
