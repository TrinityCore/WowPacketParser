using System.Reflection.Emit;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class GarrisonHandler
    {
        public static void ReadGarrisonMission(Packet packet, params object[] indexes)
        {
            packet.ReadUInt64("DbID", indexes);
            packet.ReadUInt32("MissionRecID", indexes);
            packet.ReadTime("OfferTime", indexes);
            packet.ReadUInt32("OfferDuration", indexes);
            packet.ReadTime("StartTime", indexes);
            packet.ReadUInt32("TravelDuration", indexes);
            packet.ReadUInt32("MissionDuration", indexes);
            packet.ReadUInt32("MissionState", indexes);
            packet.ReadUInt32("Unknown1", indexes);
            packet.ReadUInt32("Unknown2", indexes);
        }

        public static void ReadGarrisonMissionOvermaxRewards(Packet packet, params object[] indexes)
        {
            var missionRewardCount = packet.ReadInt32("MissionRewardCount", indexes);
            for (int i = 0; i < missionRewardCount; i++)
            {
                packet.ReadInt32<ItemId>("ItemID", indexes, i);
                packet.ReadUInt32("Quantity", indexes, i);
                packet.ReadInt32("CurrencyID", indexes, i);
                packet.ReadUInt32("CurrencyQuantity", indexes, i);
                packet.ReadUInt32("FollowerXP", indexes, i);
                packet.ReadUInt32("BonusAbilityID", indexes, i);
                packet.ReadInt32("Unknown", indexes, i);
            }
        }

        public static void ReadGarrisonBuildingInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("GarrPlotInstanceID", indexes);
            packet.ReadUInt32("GarrBuildingID", indexes);
            packet.ReadUInt32("TimeBuilt", indexes);
            packet.ReadUInt32("CurrentGarSpecID", indexes);
            packet.ReadUInt32("TimeSpecCooldown", indexes);

            packet.ResetBitReader();

            packet.ReadBit("Active", indexes);
        }

        public static void ReadGarrisonFollower(Packet packet, params object[] indexes)
        {
            packet.ReadUInt64("DbID", indexes);
            packet.ReadUInt32("GarrFollowerID", indexes);
            packet.ReadUInt32("Quality", indexes);
            packet.ReadUInt32("FollowerLevel", indexes);
            packet.ReadUInt32("ItemLevelWeapon", indexes);
            packet.ReadUInt32("ItemLevelArmor", indexes);
            packet.ReadUInt32("Xp", indexes);
            packet.ReadUInt32("Durability", indexes);
            packet.ReadUInt32("CurrentBuildingID", indexes);
            packet.ReadUInt32("CurrentMissionID", indexes);
            var abilityCount = packet.ReadInt32("AbilityCount", indexes);
            packet.ReadInt32("ZoneSupportSpellID", indexes);
            packet.ReadInt32("FollowerStatus", indexes);

            for (int i = 0; i < abilityCount; i++)
                packet.ReadInt32("AbilityID", indexes, i);

            packet.ResetBitReader();

            var len = packet.ReadBits(7);
            packet.ReadWoWString("CustomName", len, indexes);
        }

        public static void ReadGarrisonTalents(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("GarrTalentID", indexes);
            packet.ReadInt32("ResearchStartTime", indexes);
            packet.ReadInt32("Flags", indexes);
        }

        public static void ReadFollowerSoftCapInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("GarrFollowerTypeID", indexes);
            packet.ReadUInt32("Count", indexes);
        }

        [Parser(Opcode.SMSG_DISPLAY_TOAST)]
        public static void HandleDisplayToast(Packet packet)
        {
            packet.ReadUInt64("Quantity");

            packet.ReadByte("DisplayToastMethod");
            packet.ReadUInt32("QuestID");

            packet.ResetBitReader();

            packet.ReadBit("Mailed");

            var type = packet.ReadBits("Type", 2);
            if (type == 0)
            {
                packet.ReadBit("BonusRoll");
                V6_0_2_19033.Parsers.ItemHandler.ReadItemInstance(packet);
                packet.ReadInt32("SpecializationID");
                packet.ReadInt32("ItemQuantity?");
            }

            if (type == 1)
                packet.ReadInt32("CurrencyID");
        }

        [Parser(Opcode.SMSG_GET_GARRISON_INFO_RESULT)]
        public static void HandleGetGarrisonInfoResult(Packet packet)
        {
            packet.ReadInt32("FactionIndex");
            var garrisonCount = packet.ReadUInt32("GarrisonCount");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_5_24330))
            {
                var followerSoftcapCount = packet.ReadUInt32("FollowerSoftCapCount");
                for (var i = 0u; i < followerSoftcapCount; ++i)
                    ReadFollowerSoftCapInfo(packet, i);
            }

            for (int i = 0; i < garrisonCount; i++)
            {
                packet.ReadInt32("GarrTypeID", i);
                packet.ReadInt32("GarrSiteID", i);
                packet.ReadInt32("GarrSiteLevelID", i);

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
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23826) && ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_5_24330))
                    packet.ReadInt32("FollowerSoftCap", i);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_0_23826))
                    for (int j = 0; j < garrisonBuildingInfoCount; j++)
                        ReadGarrisonBuildingInfo(packet, "BuildingInfo", i, j);

                for (int j = 0; j < garrisonPlotInfoCount; j++)
                    V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonPlotInfo(packet, "PlotInfo", i, j);

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V7_2_0_23826))
                    for (int j = 0; j < garrisonFollowerCount; j++)
                        ReadGarrisonFollower(packet, "Follower", i, j);

                for (int j = 0; j < garrisonMissionCount; j++)
                    ReadGarrisonMission(packet, "Mission", i, j);

                for (int j = 0; j < garrisonMissionRewardsCount; j++)
                    ReadGarrisonMissionOvermaxRewards(packet, "MissionRewards", i, j);

                for (int j = 0; j < garrisonMissionOvermaxRewardsCount; j++)
                    ReadGarrisonMissionOvermaxRewards(packet, "MissionOvermaxRewards", i, j);

                for (int j = 0; j < areaBonusCount; j++)
                    V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonMissionAreaBonus(packet, "MissionAreaBonus", i, j);

                for (int j = 0; j < talentsCount; j++)
                    ReadGarrisonTalents(packet, "Talents", i, j);

                for (int j = 0; j < archivedMissionsCount; j++)
                    packet.ReadInt32("ArchivedMissions", i, j);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23826))
                    for (int j = 0; j < garrisonBuildingInfoCount; j++)
                        ReadGarrisonBuildingInfo(packet, "BuildingInfo", i, j);

                packet.ResetBitReader();

                for (int j = 0; j < canStartMissionCount; j++)
                    packet.ReadBit("CanStartMission", i, j);

                packet.ResetBitReader();

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23826))
                    for (int j = 0; j < garrisonFollowerCount; j++)
                        ReadGarrisonFollower(packet, "Follower", i, j);
            }
        }
    }
}
