using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientUpdateExpansionLevel
    {
        public List<RaceClassAvailability> AvailableClasses;
        public byte? ActiveExpansionLevel; // Optional
        public List<RaceClassAvailability> AvailableRaces;
    }
}
