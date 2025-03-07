using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class GarrisonHandler
    {
        [Parser(Opcode.CMSG_GARRISON_CHECK_UPGRADEABLE)]
        [Parser(Opcode.CMSG_GARRISON_GET_CLASS_SPEC_CATEGORY_INFO)]
        public static void HandleGarrisonGarrSiteID(Packet packet)
        {
            packet.ReadInt32("GarrSiteID");
        }

        [Parser(Opcode.SMSG_GET_GARRISON_INFO_RESULT, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleGetGarrisonInfoResult(Packet packet)
        {
            packet.ReadInt32("FactionIndex");
            var garrisonCount = packet.ReadUInt32("GarrisonCount");

            var followerSoftcapCount = packet.ReadUInt32("FollowerSoftCapCount");
            for (var i = 0u; i < followerSoftcapCount; ++i)
                V7_0_3_22248.Parsers.GarrisonHandler.ReadFollowerSoftCapInfo(packet, i);

            for (int i = 0; i < garrisonCount; i++)
            {
                V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrType(packet, i);
                packet.ReadInt32E<GarrisonSite>("GarrSiteID", i);
                packet.ReadInt32E<GarrisonSiteLevel>("GarrSiteLevelID", i);

                var garrisonBuildingInfoCount = packet.ReadUInt32("GarrisonBuildingInfoCount", i);
                var garrisonPlotInfoCount = packet.ReadUInt32("GarrisonPlotInfoCount", i);
                var garrisonFollowerCount = packet.ReadUInt32("GarrisonFollowerCount", i);
                var garrisonMissionCount = packet.ReadUInt32("GarrisonMissionCount", i);
                var garrisonMissionRewardsCount = packet.ReadUInt32("GarrisonMissionRewardsCount", i);
                var garrisonMissionOvermaxRewardsCount = packet.ReadUInt32("GarrisonMissionOvermaxRewardsCount", i);
                var areaBonusCount = packet.ReadUInt32("GarrisonMissionAreaBonusCount", i);
                var talentsCount = packet.ReadUInt32("Talents", i);
                var canStartMissionCount = packet.ReadUInt32("CanStartMission", i);
                var archivedMissionsCount = packet.ReadUInt32("ArchivedMissionsCount", i);

                packet.ReadInt32("NumFollowerActivationsRemaining", i);
                packet.ReadUInt32("NumMissionsStartedToday", i);

                for (int j = 0; j < garrisonPlotInfoCount; j++)
                    V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonPlotInfo(packet, "PlotInfo", i, j);

                for (int j = 0; j < garrisonMissionCount; j++)
                    V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMission(packet, "Mission", i, j);

                int[] garrisonMissionRewardItemCounts = new int[garrisonMissionRewardsCount];
                for (int j = 0; j < garrisonMissionRewardItemCounts.Length; ++j)
                    garrisonMissionRewardItemCounts[j] = packet.ReadInt32();

                for (int j = 0; j < garrisonMissionRewardItemCounts.Length; ++j)
                    for (int k = 0; k < garrisonMissionRewardItemCounts[j]; ++k)
                        V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, i, "MissionRewards", j, k);

                int[] garrisonMissionOvermaxRewardItemCounts = new int[garrisonMissionOvermaxRewardsCount];
                for (int j = 0; j < garrisonMissionOvermaxRewardItemCounts.Length; ++j)
                    garrisonMissionOvermaxRewardItemCounts[j] = packet.ReadInt32();

                for (int j = 0; j < garrisonMissionOvermaxRewardItemCounts.Length; ++j)
                    for (int k = 0; k < garrisonMissionOvermaxRewardItemCounts[j]; ++k)
                        V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, i, "MissionOvermaxRewards", j, k);

                for (int j = 0; j < areaBonusCount; j++)
                    V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonMissionBonusAbility(packet, "MissionAreaBonus", i, j);

                for (int j = 0; j < talentsCount; j++)
                    V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonTalents(packet, "Talents", i, j);

                for (int j = 0; j < archivedMissionsCount; j++)
                    packet.ReadInt32("ArchivedMissions", i, j);

                    for (int j = 0; j < garrisonBuildingInfoCount; j++)
                        V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonBuildingInfo(packet, "BuildingInfo", i, j);

                packet.ResetBitReader();

                for (int j = 0; j < canStartMissionCount; j++)
                    packet.ReadBit("CanStartMission", i, j);

                packet.ResetBitReader();

                for (int j = 0; j < garrisonFollowerCount; j++)
                    V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonFollower(packet, "Follower", i, j);
            }
        }

        [Parser(Opcode.SMSG_GARRISON_RESEARCH_TALENT_RESULT)]
        public static void HandleGarrisonResearchTalentResult(Packet packet)
        {
            packet.ReadInt32("Result"); // if > 0 entire packet is unhandled
            V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrType(packet);
            packet.ReadBit("DontAlert");
            V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonTalents(packet, "Talent");
        }

        [Parser(Opcode.SMSG_GARRISON_TALENT_COMPLETED)]
        public static void HandleGarrisonTalentCompleted(Packet packet)
        {
            V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrType(packet);
            packet.ReadInt32("GarrTalentID");
            packet.ReadInt32("GarrTalentRank");
            packet.ReadInt32("Flags");
        }
    }
}
