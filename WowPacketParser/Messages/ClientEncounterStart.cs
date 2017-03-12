using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientEncounterStart
    {
        public int DifficultyID;
        public int GroupSize;
        public int EncounterID;
    }
}
