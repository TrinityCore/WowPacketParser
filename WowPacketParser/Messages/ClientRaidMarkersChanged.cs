using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRaidMarkersChanged
    {
        public uint ActiveMarkers;
        public byte PartyIndex;
        public List<CliRaidMarkerData> MarkerData;
    }
}
