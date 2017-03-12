using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetBattlePVPChallenge
    {
        public ulong ChallengerGUID;
        public PetBattleLocations Location;
    }
}
