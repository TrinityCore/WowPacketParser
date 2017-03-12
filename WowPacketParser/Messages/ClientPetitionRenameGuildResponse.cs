using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetitionRenameGuildResponse
    {
        public ulong PetitionGuid;
        public string NewGuildName;
    }
}
