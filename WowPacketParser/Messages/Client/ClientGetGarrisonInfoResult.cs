using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGetGarrisonInfoResult
    {
        public List<int> ArchivedMissions;
        public int GarrSiteID;
        public int FactionIndex;
        public List<GarrisonMission> Missions;
        public List<GarrisonBuildingInfo> Buildings;
        public List<GarrisonFollower> Followers;
        public List<GarrisonPlotInfo> Plots;
        public int GarrSiteLevelID;
    }
}
