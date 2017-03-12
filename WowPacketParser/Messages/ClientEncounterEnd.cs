using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientEncounterEnd
    {
        public int GroupSize;
        public bool Success;
        public int DifficultyID;
        public int EncounterID;
    }
}
