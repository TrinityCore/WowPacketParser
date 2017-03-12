using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientQueryPetitionResponse
    {
        public uint PetitionID;
        public bool Allow;
        public CliPetitionInfo Info;
    }
}
