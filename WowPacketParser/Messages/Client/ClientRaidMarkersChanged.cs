using System.Collections.Generic;
using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientRaidMarkersChanged
    {
        public uint ActiveMarkers;
        public byte PartyIndex;
        public List<CliRaidMarkerData> MarkerData;
    }
}
