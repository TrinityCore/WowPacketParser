using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliGarrisonRemoteSiteInfo
    {
        public int GarrSiteLevelID;
        public List<GarrisonRemoteBuildingInfo> Buildings;
    }
}
