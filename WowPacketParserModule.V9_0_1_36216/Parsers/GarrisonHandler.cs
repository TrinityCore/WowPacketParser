﻿using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class GarrisonHandler
    {
        public static void ReadGarrisonBuildingInfo(Packet packet, params object[] indexes)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
            {
                ReadGarrisonBuildingInfo905(packet, indexes);
                return;
            }

            V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonBuildingInfo(packet, indexes);
        }

        public static void ReadGarrisonBuildingInfo905(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("GarrPlotInstanceID", indexes);
            packet.ReadInt32("GarrBuildingID", indexes);
            packet.ReadTime64("TimeBuilt", indexes);
            packet.ReadInt32("CurrentGarSpecID", indexes);
            packet.ReadTime64("TimeSpecCooldown", indexes);

            packet.ResetBitReader();

            packet.ReadBit("Active", indexes);
        }

        public static void ReadGarrisonMission(Packet packet, params object[] indexes)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
            {
                ReadGarrisonMission905(packet, indexes);
                return;
            }

            packet.ReadUInt64("DbID", indexes);
            packet.ReadInt32("MissionRecID", indexes);
            packet.ReadTime("OfferTime", indexes);
            packet.ReadInt32("OfferDuration", indexes);
            packet.ReadTime("StartTime", indexes);
            packet.ReadInt32("TravelDuration", indexes);
            packet.ReadInt32("MissionDuration", indexes);
            packet.ReadUInt32E<GarrisonMissionState>("MissionState", indexes);
            packet.ReadInt32("SuccessChance", indexes);
            packet.ReadUInt32E<GarrisonMissionFlag>("Flags", indexes);
            packet.ReadSingle("MissionScalar", indexes);
        }

        private static void ReadGarrisonMission905(Packet packet, params object[] indexes)
        {
            packet.ReadUInt64("DbID", indexes);
            packet.ReadTime64("OfferTime", indexes);
            packet.ReadInt64("OfferDuration", indexes);
            packet.ReadTime64("StartTime", indexes);
            packet.ReadInt64("TravelDuration", indexes);
            packet.ReadInt64("MissionDuration", indexes);
            packet.ReadInt32("MissionRecID", indexes);
            packet.ReadInt32E<GarrisonMissionState>("MissionState", indexes);
            packet.ReadInt32("SuccessChance", indexes);
            packet.ReadUInt32E<GarrisonMissionFlag>("Flags", indexes);
            packet.ReadSingle("MissionScalar", indexes);
        }

        public static void ReadGarrisonFollower(Packet packet, params object[] indexes)
        {
            packet.ReadUInt64("DbID", indexes);
            packet.ReadInt32("GarrFollowerID", indexes);
            packet.ReadInt32E<GarrisonFollowerQuality>("Quality", indexes);
            packet.ReadInt32("FollowerLevel", indexes);
            packet.ReadInt32("ItemLevelWeapon", indexes);
            packet.ReadInt32("ItemLevelArmor", indexes);
            packet.ReadInt32("Xp", indexes);
            packet.ReadInt32("Durability", indexes);
            packet.ReadInt32("CurrentBuildingID", indexes);
            packet.ReadInt32("CurrentMissionID", indexes);
            var abilityCount = packet.ReadUInt32("AbilityCount", indexes);
            packet.ReadInt32("ZoneSupportSpellID", indexes);
            packet.ReadInt32E<GarrisonFollowerStatus>("FollowerStatus", indexes);
            packet.ReadInt32("Health", indexes);
            packet.ReadSByte("BoardIndex", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
                packet.ReadInt64("HealingTimestamp", indexes);
            else
                packet.ReadInt32("HealingTimestamp", indexes);

            for (int i = 0; i < abilityCount; i++)
                packet.ReadInt32("AbilityID", indexes, i);

            packet.ResetBitReader();

            var len = packet.ReadBits(7);
            packet.ReadWoWString("CustomName", len, indexes);
        }

        public static void ReadGarrisonMissionBonusAbility(Packet packet, params object[] indexes)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
            {
                ReadGarrisonMissionBonusAbility905(packet, indexes);
                return;
            }

            V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonMissionBonusAbility(packet, indexes);
        }

        private static void ReadGarrisonMissionBonusAbility905(Packet packet, params object[] indexes)
        {
            packet.ReadTime64("StartTime", indexes);
            packet.ReadInt32("GarrMssnBonusAbilityID", indexes);
        }

        public static void ReadGarrisonTalentSocketData(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("SoulbindConduitID", indexes);
            packet.ReadInt32("SoulbindConduitRank", indexes);
        }

        public static void ReadGarrisonTalents(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadInt32("GarrTalentID", indexes);
            packet.ReadInt32("Rank", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
                packet.ReadInt64("ResearchStartTime", indexes);
            else
                packet.ReadInt32("ResearchStartTime", indexes);
            packet.ReadInt32("Flags", indexes);
            var hasSocket = packet.ReadBit();
            if (hasSocket)
                ReadGarrisonTalentSocketData(packet, indexes, "Socket");
        }

        public static void ReadGarrisonCollectionEntry(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("EntryID", indexes);
            packet.ReadInt32("Rank", indexes);
        }

        public static void ReadGarrisonCollection(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("EntryID", indexes);
            var count = packet.ReadUInt32();
            for (var i = 0u; i < count; ++i)
                ReadGarrisonCollectionEntry(packet, indexes, i);
        }

        public static void ReadGarrisonEventEntry(Packet packet, params object[] indexes)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
            {
                ReadGarrisonEventEntry905(packet, indexes);
                return;
            }

            packet.ReadInt32("EntryID", indexes);
            packet.ReadInt32("EventValue", indexes);
        }

        private static void ReadGarrisonEventEntry905(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("EventValue", indexes);
            packet.ReadInt32("EntryID", indexes);
        }

        public static void ReadGarrisonEventList(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("EntryID", indexes);
            var count = packet.ReadUInt32();
            for (var i = 0u; i < count; ++i)
                ReadGarrisonEventEntry(packet, indexes, i);
        }

        public static void ReadGarrisonSpecGroup(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ChrSpecializationID", indexes);
            packet.ReadInt32("SoulbindID", indexes);
        }

        public static void ReadGarrisonStartMissionFollowerInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt64("DatabaseID", indexes);
            packet.ReadUInt32("BoardIndex", indexes);
            packet.ReadUInt32("Health", indexes);
            packet.ResetBitReader();

            var hasFollowerEntry = packet.ReadBit("HasFollowerEntry", indexes);
            if (hasFollowerEntry)
                packet.ReadUInt32("FollowerEntry", indexes);
        }

        [Parser(Opcode.SMSG_GET_GARRISON_INFO_RESULT)]
        public static void HandleGetGarrisonInfoResult(Packet packet)
        {
            packet.ReadInt32("FactionIndex");
            var garrisonCount = packet.ReadUInt32("GarrisonCount");

            var followerSoftcapCount = packet.ReadUInt32("FollowerSoftCapCount");
            for (var i = 0u; i < followerSoftcapCount; ++i)
                V7_0_3_22248.Parsers.GarrisonHandler.ReadFollowerSoftCapInfo(packet, i);

            for (int i = 0; i < garrisonCount; i++)
            {
                packet.ReadInt32E<GarrisonType>("GarrTypeID", i);
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

                uint specGroupsCount = 0;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                    specGroupsCount = packet.ReadUInt32("SpecGroupsCount", i);

                var canStartMissionCount = packet.ReadUInt32("CanStartMission", i);
                var archivedMissionsCount = packet.ReadUInt32("ArchivedMissionsCount", i);

                packet.ReadInt32("NumFollowerActivationsRemaining", i);
                packet.ReadUInt32("NumMissionsStartedToday", i);
                packet.ReadInt32("MinAutoTroopLevel", i);

                for (int j = 0; j < garrisonPlotInfoCount; j++)
                    V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonPlotInfo(packet, i, "PlotInfo", j);

                for (int j = 0; j < garrisonMissionCount; j++)
                    ReadGarrisonMission(packet, i, "Mission", j);

                int[] garrisonMissionRewardItemCounts = new int[garrisonMissionRewardsCount];
                for (int j = 0; j < garrisonMissionRewardItemCounts.Length; ++j)
                    garrisonMissionRewardItemCounts[j] = packet.ReadInt32();

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_0_2_36639))
                    for (int j = 0; j < garrisonMissionRewardItemCounts.Length; ++j)
                        for (int k = 0; k < garrisonMissionRewardItemCounts[j]; ++k)
                            V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, i, "MissionRewards", j, k);

                int[] garrisonMissionOvermaxRewardItemCounts = new int[garrisonMissionOvermaxRewardsCount];
                for (int j = 0; j < garrisonMissionOvermaxRewardItemCounts.Length; ++j)
                    garrisonMissionOvermaxRewardItemCounts[j] = packet.ReadInt32();

                if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_0_2_36639))
                    for (int j = 0; j < garrisonMissionOvermaxRewardItemCounts.Length; ++j)
                        for (int k = 0; k < garrisonMissionOvermaxRewardItemCounts[j]; ++k)
                            V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, i, "MissionOvermaxRewards", j, k);

                for (int j = 0; j < areaBonusCount; j++)
                    ReadGarrisonMissionBonusAbility(packet, i, "MissionAreaBonus", j);

                for (int j = 0; j < collectionsCount; j++)
                    ReadGarrisonCollection(packet, i, "Collection", j);

                for (int j = 0; j < eventListCount; j++)
                    ReadGarrisonEventList(packet, i, "EventList", j);

                for (int j = 0; j < specGroupsCount; j++)
                    ReadGarrisonSpecGroup(packet, i, "SpecGroup", j);

                for (int j = 0; j < archivedMissionsCount; j++)
                    packet.ReadInt32("ArchivedMissions", i, j);

                for (int j = 0; j < garrisonBuildingInfoCount; j++)
                    ReadGarrisonBuildingInfo(packet, i, "BuildingInfo", j);

                packet.ResetBitReader();

                for (int j = 0; j < canStartMissionCount; j++)
                    packet.ReadBit("CanStartMission", i, j);

                for (int j = 0; j < garrisonFollowerCount; j++)
                    ReadGarrisonFollower(packet, i, "Follower", j);

                for (int j = 0; j < autoTroopCount; j++)
                    ReadGarrisonFollower(packet, i, "AutoTroop", j);

                for (int j = 0; j < talentsCount; j++)
                    ReadGarrisonTalents(packet, i, "Talents", j);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_2_36639))
                {
                    for (int j = 0; j < garrisonMissionRewardItemCounts.Length; ++j)
                        for (int k = 0; k < garrisonMissionRewardItemCounts[j]; ++k)
                            V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, i, "MissionRewards", j, k);

                    for (int j = 0; j < garrisonMissionOvermaxRewardItemCounts.Length; ++j)
                        for (int k = 0; k < garrisonMissionOvermaxRewardItemCounts[j]; ++k)
                            V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, i, "MissionOvermaxRewards", j, k);
                }
            }
        }

        [Parser(Opcode.SMSG_GARRISON_ADD_MISSION_RESULT)]
        public static void HandleGarrisonAddMissionResult(Packet packet)
        {
            packet.ReadInt32E<GarrisonType>("GarrTypeId");
            packet.ReadInt32E<GarrisonResult>("Result");
            packet.ReadByte("State");

            ReadGarrisonMission(packet, "Mission");

            uint rewardCount1 = packet.ReadUInt32("RewardsCount");
            uint rewardCount2 = packet.ReadUInt32("OvermaxRewardsCount");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_2_36639))
            {
                packet.ResetBitReader();
                packet.ReadBit("CanStart");
            }

            for (int i = 0; i < rewardCount1; i++)
                V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, "MissionReward", i);

            for (int i = 0; i < rewardCount2; i++)
                V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, "MissionOvermaxReward", i);

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V9_0_2_36639))
            {
                packet.ResetBitReader();
                packet.ReadBit("CanStart");
            }
        }

        [Parser(Opcode.SMSG_COVENANT_CALLINGS_AVAILABILITY_RESPONSE)]
        public static void HandleGarrisonCovenantCallingsAvailability(Packet packet)
        {
            packet.ResetBitReader();
            packet.ReadBit("AreCallingsUnlocked");
            int bountyCount = packet.ReadInt32();

            for (int i = 0; i < bountyCount; i++)
                packet.ReadInt32("BountyID", i);
        }

        [Parser(Opcode.SMSG_COVENANT_RENOWN_OPEN_NPC)]
        public static void HandleGarrisonCovenantRenownOpenNpc(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_5_37503))
            {
                packet.ResetBitReader();
                packet.ReadBit("CatchupState");
            }
        }

        [Parser(Opcode.SMSG_COVENANT_RENOWN_SEND_CATCHUP_STATE)]
        public static void HandleGarrisonCovenantRenownSendCatchupState(Packet packet)
        {
            packet.ResetBitReader();
            packet.ReadBit("CatchupState");
        }

        [Parser(Opcode.CMSG_GARRISON_START_MISSION)]
        public static void HandleGarrisonStartMission(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");

            var infoCount = packet.ReadInt32("InfoCount");
            packet.ReadInt32("MissionRecID");

            for (int i = 0; i < infoCount; i++)
                ReadGarrisonStartMissionFollowerInfo(packet, i, "FollowerInfo");
        }
        
        [Parser(Opcode.SMSG_OPEN_ANIMA_DIVERSION_UI)]
        public static void HandleAnimaOpenUI(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadInt32("UiMapID");
            packet.ReadInt32("GarrTalentTreeID");
        }
    }
}
