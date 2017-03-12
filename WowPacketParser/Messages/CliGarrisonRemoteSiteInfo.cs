using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliGarrisonRemoteSiteInfo
    {
        public int GarrSiteLevelID;
        public List<GarrisonRemoteBuildingInfo> Buildings;
    }
}
