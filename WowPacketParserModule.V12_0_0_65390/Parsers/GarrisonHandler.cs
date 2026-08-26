using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V12_0_0_65390.Parsers
{
    public static class GarrisonHandler
    {
        [Parser(Opcode.SMSG_GET_GARRISON_INFO_RESULT, ClientVersionBuild.V12_1_0_69214)]
        public static void HandleGetGarrisonInfoResult(Packet packet)
        {
            packet.ReadSByte("FactionIndex");

            var garrisonCount = packet.ReadUInt32("GarrisonCount");
            var followerSoftcapCount = packet.ReadUInt32("FollowerSoftCapCount");

            for (var i = 0; i < garrisonCount; i++)
            {
                V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrType(packet, i);
                packet.ReadInt32E<GarrisonSite>("GarrSiteID", i);
                packet.ReadInt32E<GarrisonSiteLevel>("GarrSiteLevelID", i);

                var garrisonBuildingInfoCount = packet.ReadUInt32("GarrisonBuildingInfoCount", i);
                var garrisonPlotInfoCount = packet.ReadUInt32("GarrisonPlotInfoCount", i);
                var garrisonFollowerCount = packet.ReadUInt32("GarrisonFollowerCount", i);
                var autoTroopCount = packet.ReadUInt32("GarrisonAutoTroopCount", i);
                var garrisonMissionCount = packet.ReadUInt32("GarrisonMissionCount", i);
                var garrisonMissionRewardsCount = packet.ReadUInt32("GarrisonMissionRewardsCount", i);
                var garrisonMissionOvermaxRewardsCount = packet.ReadUInt32("GarrisonMissionOvermaxRewardsCount", i);
                var areaBonusCount = packet.ReadUInt32("GarrisonMissionAreaBonusCount", i);
                var talentsCount = packet.ReadUInt32("Talents", i);
                var collectionsCount = packet.ReadUInt32("GarrisonCollectionCount", i);
                var eventListCount = packet.ReadUInt32("GarrisonEventListCount", i);
                var specGroupsCount = packet.ReadUInt32("SpecGroupsCount", i);
                var canStartMissionCount = packet.ReadUInt32("CanStartMission", i);
                var archivedMissionsCount = packet.ReadUInt32("ArchivedMissionsCount", i);

                packet.ReadInt32("NumFollowerActivationsRemaining", i);
                packet.ReadUInt32("NumMissionsStartedToday", i);
                packet.ReadInt32("MinAutoTroopLevel", i);

                for (var j = 0; j < garrisonBuildingInfoCount; j++)
                    V9_0_1_36216.Parsers.GarrisonHandler.ReadGarrisonBuildingInfo(packet, i, "BuildingInfo", j);

                for (var j = 0; j < garrisonPlotInfoCount; j++)
                    V9_0_1_36216.Parsers.GarrisonHandler.ReadGarrisonPlotInfo(packet, i, "PlotInfo", j);

                for (var j = 0; j < garrisonFollowerCount; j++)
                    V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonFollower(packet, i, "Follower", j);

                for (var j = 0; j < autoTroopCount; j++)
                    V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonFollower(packet, i, "AutoTroop", j);

                for (var j = 0; j < garrisonMissionCount; j++)
                    V9_0_1_36216.Parsers.GarrisonHandler.ReadGarrisonMission(packet, i, "Mission", j);

                for (var j = 0; j < garrisonMissionRewardsCount; ++j)
                {
                    var itemCount = packet.ReadInt32();
                    for (var k = 0; k < itemCount; ++k)
                        V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, i, "MissionRewards", j, k);
                }

                for (var j = 0; j < garrisonMissionOvermaxRewardsCount; ++j)
                {
                    var itemCount = packet.ReadInt32();
                    for (var k = 0; k < itemCount; ++k)
                        V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, i, "MissionOvermaxRewards", j, k);
                }

                for (var j = 0; j < areaBonusCount; j++)
                    V9_0_1_36216.Parsers.GarrisonHandler.ReadGarrisonMissionBonusAbility(packet, i, "MissionAreaBonus", j);

                for (var j = 0; j < talentsCount; j++)
                    V9_0_1_36216.Parsers.GarrisonHandler.ReadGarrisonTalents(packet, i, "Talents", j);

                for (var j = 0; j < collectionsCount; j++)
                    V9_0_1_36216.Parsers.GarrisonHandler.ReadGarrisonCollection(packet, i, "Collection", j);

                for (var j = 0; j < eventListCount; j++)
                    V9_0_1_36216.Parsers.GarrisonHandler.ReadGarrisonEventList(packet, i, "EventList", j);

                for (var j = 0; j < specGroupsCount; j++)
                    V9_0_1_36216.Parsers.GarrisonHandler.ReadGarrisonSpecGroup(packet, i, "SpecGroup", j);

                for (var j = 0; j < archivedMissionsCount; j++)
                    packet.ReadInt32("ArchivedMissions", i, j);

                packet.ResetBitReader();

                for (var j = 0; j < canStartMissionCount; j++)
                    packet.ReadBit("CanStartMission", i, j);
            }

            for (var i = 0u; i < followerSoftcapCount; ++i)
                V9_0_1_36216.Parsers.GarrisonHandler.ReadFollowerSoftCapInfo(packet, i);
        }
    }
}
