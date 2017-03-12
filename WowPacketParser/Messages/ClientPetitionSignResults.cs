using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetitionSignResults
    {
        public ulong Player;
        public PetitionError Error;
        public ulong Item;
    }
}
