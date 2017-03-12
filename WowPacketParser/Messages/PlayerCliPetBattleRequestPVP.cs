using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetBattleRequestPVP
    {
        public PetBattleLocations Location;
        public ulong OpponentCharacterID;
    }
}
