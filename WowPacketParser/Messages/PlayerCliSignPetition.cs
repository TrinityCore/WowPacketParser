using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSignPetition
    {
        public ulong PetitionGUID;
        public byte Choice;
    }
}
