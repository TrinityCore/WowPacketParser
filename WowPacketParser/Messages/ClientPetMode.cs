using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetMode
    {
        public ulong PetGUID;
        public uint PetMode;
    }
}
