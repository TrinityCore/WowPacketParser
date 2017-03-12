using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetStableList
    {
        public List<ClientPetStableInfo> Pets;
        public ulong StableMaster;
    }
}
