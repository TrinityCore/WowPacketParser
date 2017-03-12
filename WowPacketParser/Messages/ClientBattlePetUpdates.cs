using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePetUpdates
    {
        public bool AddedPet;
        public List<ClientBattlePet> Pets;
    }
}
