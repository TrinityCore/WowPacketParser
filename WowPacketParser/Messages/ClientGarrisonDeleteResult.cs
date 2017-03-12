using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGarrisonDeleteResult
    {
        public int Result;
        public int GarrSiteID;
    }
}
