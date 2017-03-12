using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliGarrisonRemoteSiteInfo
    {
        public int GarrSiteLevelID;
        public List<GarrisonRemoteBuildingInfo> Buildings;
    }
}
