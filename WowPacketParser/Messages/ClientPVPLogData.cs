using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPVPLogData
    {
        public byte? Winner; // Optional
        public List<ClientPVPLogData_Player> Players;
        public ClientPVPLogData_RatingData? Ratings; // Optional
        public fixed sbyte PlayerCount[2];
    }
}
