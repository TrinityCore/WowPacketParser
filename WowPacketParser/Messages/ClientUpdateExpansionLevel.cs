using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUpdateExpansionLevel
    {
        public List<RaceClassAvailability> AvailableClasses;
        public byte? ActiveExpansionLevel; // Optional
        public List<RaceClassAvailability> AvailableRaces;
    }
}
