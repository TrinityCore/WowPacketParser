using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPVPLogData_RatingData
    {
        public fixed int Prematch[2];
        public fixed int Postmatch[2];
        public fixed int PrematchMMR[2];
    }
}
