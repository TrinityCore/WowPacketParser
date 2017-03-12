using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
