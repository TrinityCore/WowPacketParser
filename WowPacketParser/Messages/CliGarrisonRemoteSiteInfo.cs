using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliGarrisonRemoteSiteInfo
    {
        public int GarrSiteLevelID;
        public List<GarrisonRemoteBuildingInfo> Buildings;
    }
}
