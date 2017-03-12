using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInstanceEncounterObjectiveUpdate
    {
        public int ProgressAmount;
        public int ObjectiveID;
    }
}
