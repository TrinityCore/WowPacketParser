using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetStableList
    {
        public List<ClientPetStableInfo> Pets;
        public ulong StableMaster;
    }
}
