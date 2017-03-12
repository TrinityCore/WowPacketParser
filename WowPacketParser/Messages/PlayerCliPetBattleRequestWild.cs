using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetBattleRequestWild
    {
        public PetBattleLocations Location;
        public ulong TargetGUID;
    }
}
