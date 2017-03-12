using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePetUpdates
    {
        public bool AddedPet;
        public List<ClientBattlePet> Pets;
    }
}
