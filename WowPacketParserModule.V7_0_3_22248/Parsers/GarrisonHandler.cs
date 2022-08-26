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
            packet.ReadInt32("MissionRecID", indexes);
            packet.ReadTime("OfferTime", indexes);
            packet.ReadInt32("OfferDuration", indexes);
            packet.ReadTime("StartTime", indexes);
            packet.ReadInt32("TravelDuration", indexes);
            packet.ReadInt32("MissionDuration", indexes);
            packet.ReadUInt32E<GarrisonMissionState>("MissionState", indexes);
            packet.ReadInt32("SuccessChance", indexes);
            packet.ReadUInt32E<GarrisonMissionFlag>("Flags", indexes);
        }

        public static void ReadGarrisonMissionOvermaxRewards(Packet packet, params object[] indexes)
        {
            var missionRewardCount = packet.ReadInt32("MissionRewardCount", indexes);
            for (int i = 0; i < missionRewardCount; i++)
                ReadGarrisonMissionReward(packet, indexes, i);
        }

        public static void ReadGarrisonBuildingInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("GarrPlotInstanceID", indexes);
            packet.ReadInt32("GarrBuildingID", indexes);
            packet.ReadTime("TimeBuilt", indexes);
            packet.ReadInt32("CurrentGarSpecID", indexes);
            packet.ReadInt32("TimeSpecCooldown", indexes);

            packet.ResetBitReader();

            packet.ReadBit("Active", indexes);
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

            for (int i = 0; i < abilityCount; i++)
                packet.ReadInt32("AbilityID", indexes, i);

            packet.ResetBitReader();

            var len = packet.ReadBits(7);
            packet.ReadWoWString("CustomName", len, indexes);
        }

        public static void ReadGarrisonTalents(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("GarrTalentID", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_3_0_33062))
                packet.ReadInt32("Rank", indexes);
            packet.ReadInt32("ResearchStartTime", indexes);
            packet.ReadInt32("Flags", indexes);
        }

        public static void ReadFollowerSoftCapInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("GarrFollowerTypeID", indexes);
            packet.ReadUInt32("Count", indexes);
        }

        public static void ReadGarrisonShipment(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ShipmentRecId", indexes);
            packet.ReadUInt64("ShipmentId", indexes);
            packet.ReadUInt64("AssignedFollowerDBID", indexes);
            packet.ReadTime("CreationTime", indexes);
            packet.ReadInt32("ShipmentDuration", indexes);
            packet.ReadInt32("BuildingType", indexes);
        }

        public static void ReadGarrisonMissionReward(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadInt32<ItemId>("ItemID", indexes);
            packet.ReadUInt32("ItemQuantity", indexes);
            packet.ReadInt32<CurrencyId>("CurrencyID", indexes);
            packet.ReadUInt32("CurrencyQuantity", indexes);
            packet.ReadUInt32("FollowerXP", indexes);
            packet.ReadUInt32("GarrMssnBonusAbilityID", indexes);
            packet.ReadInt32("ItemFileDataID", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_0_2_36639))
            {
                if (packet.ReadBit())
                    Substructures.ItemHandler.ReadItemInstance(packet, indexes, "ItemInstance");
            }
        }

        public static void ReadGarrisonFollowerCategoryInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("GarrClassSpecId", indexes);
            packet.ReadInt32("GarrClassSpecPlayerCondId", indexes);
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
                Substructures.ItemHandler.ReadItemInstance(packet);
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
                packet.ReadInt32E<GarrisonType>("GarrTypeID", i);
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
                    V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonMissionBonusAbility(packet, "MissionAreaBonus", i, j);

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

        [Parser(Opcode.SMSG_GARRISON_REQUEST_BLUEPRINT_AND_SPECIALIZATION_DATA_RESULT)]
        public static void HandleGarrisonRequestBlueprintAndSpecializationDataResult(Packet packet)
        {
            packet.ReadInt32E<GarrisonType>("GarrTypeID");
            var int8 = packet.ReadUInt32("SpecializationsKnownCount");
            var int4 = packet.ReadUInt32("BlueprintsKnownCount");

            for (var i = 0; i < int8; i++)
                packet.ReadInt32("SpecializationsKnown", i);

            for (var i = 0; i < int4; i++)
                packet.ReadInt32("BlueprintsKnown", i);
        }

        [Parser(Opcode.SMSG_GARRISON_ADD_FOLLOWER_RESULT)]
        public static void HandleGarrisonAddFollowerResult(Packet packet)
        {
            packet.ReadInt32E<GarrisonType>("GarrTypeID");
            packet.ReadInt32E<GarrisonResult>("Result");
            ReadGarrisonFollower(packet);
        }

        [Parser(Opcode.SMSG_GARRISON_LEARN_BLUEPRINT_RESULT)]
        public static void HandleGarrisonLearnBlueprintResult(Packet packet)
        {
            packet.ReadInt32E<GarrisonType>("GarrTypeID");
            packet.ReadInt32E<GarrisonResult>("Result");
            packet.ReadInt32("BuildingID");
        }

        [Parser(Opcode.SMSG_GARRISON_PLACE_BUILDING_RESULT)]
        public static void HandleGarrisonPlaceBuildingResult(Packet packet)
        {
            packet.ReadInt32E<GarrisonType>("GarrTypeID");
            packet.ReadInt32E<GarrisonResult>("Result");
            ReadGarrisonBuildingInfo(packet, "BuildingInfo");

            packet.ResetBitReader();

            packet.ReadBit("PlayActivationCinematic");
        }

        [Parser(Opcode.SMSG_GARRISON_START_MISSION_RESULT)]
        public static void HandleGarrisonStartMissionResult(Packet packet)
        {
            packet.ReadInt32E<GarrisonMissionResult>("Result");
            packet.ReadUInt16("SessionMissionCount");
            ReadGarrisonMission(packet, "Mission");

            var followerCount = packet.ReadUInt32("FollowerCount");
            for (int i = 0; i < followerCount; i++)
                packet.ReadUInt64("FollowerDBIDs", i);
        }

        [Parser(Opcode.SMSG_GARRISON_FOLLOWER_CHANGED_XP)]
        public static void HandleGarrisonFollowerChangedXp(Packet packet)
        {
            packet.ReadInt32("XPEarned");
            packet.ReadInt32("XPSource");
            ReadGarrisonFollower(packet, "FollowerBefore");
            ReadGarrisonFollower(packet, "FollowerAfter");
        }

        [Parser(Opcode.CMSG_OPEN_MISSION_NPC)]
        public static void HandleGarrisonNpcGUID(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadInt32E<GarrisonFollowerType>("FollowerType"); // Indicates which type of missions
        }

        [Parser(Opcode.SMSG_GARRISON_OPEN_MISSION_NPC)]
        public static void HandleGarrisonOpenMissionNpc(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGuid");
            packet.ReadInt32E<GarrisonFollowerType>("FollowerType"); // Indicates which type of missions
        }

        [Parser(Opcode.SMSG_GET_LANDING_PAGE_SHIPMENTS_RESPONSE)]
        public static void HandleGetLandingPageShipmentsResponse(Packet packet)
        {
            if(ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_23706))
                packet.ReadUInt32("UnkUInt32");

            uint shipmentsCount = packet.ReadUInt32("ShipmentsCount");
            for (uint i = 0; i < shipmentsCount; i++)
                ReadGarrisonShipment(packet, "Shipment", i);
        }

        [Parser(Opcode.SMSG_GARRISON_MISSION_BONUS_ROLL_RESULT)]
        public static void HandleGarrisonMissionBonusRollResult(Packet packet)
        {
            ReadGarrisonMission(packet, "Mission");
            packet.ReadInt32("MissionRecID");
            packet.ReadInt32E<GarrisonMissionResult>("Result");
        }

        [Parser(Opcode.SMSG_GARRISON_COMPLETE_MISSION_RESULT)]
        public static void HandleGarrisonFinishMission(Packet packet)
        {
            packet.ReadInt32E<GarrisonMissionResult>("Result");
            ReadGarrisonMission(packet, "Mission");
            packet.ReadInt32("MissionRecId");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_2_0_24015)) // even earlier?
            {
                uint count = packet.ReadUInt32("MissionFollowerCount");
                for (uint i = 0; i < count; i++)
                {
                    packet.ReadUInt64("FollowerDBID", i);
                    packet.ReadUInt32("Flags", i);
                }
                packet.ReadBit("Succeeded");
            }
        }

        [Parser(Opcode.SMSG_GARRISON_MISSION_START_CONDITION_UPDATE)]
        public static void HandleGarrisonMissionStartConditionUpdate(Packet packet)
        {
            uint missionsCount = packet.ReadUInt32("MissionsCount");
            uint canStartMissionCount = packet.ReadUInt32("CanStartMissionCount");

            for (uint i = 0; i < missionsCount; i++)
                packet.ReadInt32("MissionRecID", i);

            for (uint i = 0; i < canStartMissionCount; i++)
                packet.ReadBit("CanStartMission", i);
        }

        [Parser(Opcode.SMSG_GARRISON_GET_CLASS_SPEC_CATEGORY_INFO_RESULT)]
        public static void HandleGarrisonGetClassSpecCategoryInfoResult(Packet packet)
        {
            packet.ReadInt32E<GarrisonFollowerType>("FollowerTypeId");
            uint count = packet.ReadUInt32("CategoryInfoCount");
            for (uint i = 0; i < count; i++)
                ReadGarrisonFollowerCategoryInfo(packet, "FollowerCategoryInfo", i);
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

            for (int i = 0; i < rewardCount1; i++)
                ReadGarrisonMissionReward(packet, "MissionReward", i);

            for (int i = 0; i < rewardCount2; i++)
                ReadGarrisonMissionReward(packet, "MissionOvermaxReward", i);

            packet.ResetBitReader();
            packet.ReadBit("CanStart");
        }

        [Parser(Opcode.CMSG_ADVENTURE_MAP_START_QUEST)]
        public static void HandleAdventureMapStartQuest(Packet packet)
        {
            packet.ReadInt32<QuestId>("QuestID");
        }

        [Parser(Opcode.SMSG_GARRISON_FOLLOWER_FATIGUE_CLEARED)]
        public static void HandleGarrisonFollowerFatigueCleared(Packet packet)
        {
            packet.ReadInt32E<GarrisonType>("GarrTypeID");
            packet.ReadInt32E<GarrisonResult>("Result");
        }

        [Parser(Opcode.SMSG_DELETE_EXPIRED_MISSIONS_RESULT)]
        public static void HandleDeleteExpiredMissionsResult(Packet packet)
        {
            packet.ReadUInt32E<GarrisonType>("GarrTypeID");
            packet.ReadUInt32E<GarrisonResult>("Result");
            var removedMissionsCount = packet.ReadInt32("RemovedMissionsCount");
            for (int i = 0; i < removedMissionsCount; i++)
                packet.ReadUInt32("RemovedMissions", i);

            packet.ReadBit("Succeeded");
            packet.ReadBit("LegionUnkBit");
        }

        [Parser(Opcode.SMSG_GARRISON_UPDATE_FOLLOWER)]
        public static void HandleGarrisonUpdateFollower(Packet packet)
        {
            packet.ReadUInt32("Unk1");
            ReadGarrisonFollower(packet, "Follower");
        }

        [Parser(Opcode.SMSG_GARRISON_FOLLOWER_CHANGED_QUALITY)]
        public static void HandleGarrisonFollowerChangedQuality(Packet packet)
        {
            ReadGarrisonFollower(packet, "Follower");
        }

        [Parser(Opcode.CMSG_GARRISON_RESEARCH_TALENT)]
        public static void HandleGarrisonResearchTalent(Packet packet)
        {
            packet.ReadInt32("GarrTalentID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_3_0_33062))
                packet.ReadInt32E<GarrisonTalentFlag>("Flags");
        }

        [Parser(Opcode.CMSG_GARRISON_SET_BUILDING_ACTIVE)]
        public static void HandleGarrisonSetBuildingActive(Packet packet)
        {
            packet.ReadUInt32("BuildingId");
        }

        [Parser(Opcode.SMSG_GARRISON_FOLLOWER_ACTIVATIONS_SET)]
        public static void HandleGarrisonFollowerActivationsSet(Packet packet)
        {
            packet.ReadUInt32E<GarrisonSite>("GarrSiteID");
            packet.ReadUInt32("NumActivations");
        }

        [Parser(Opcode.CMSG_GARRISON_SET_FOLLOWER_FAVORITE)]
        public static void HandleGarrisonSetFollowerFavorite(Packet packet)
        {
            packet.ReadUInt64("FollowerDbId");
            packet.ReadBit("Favorite");
        }

        [Parser(Opcode.CMSG_GARRISON_CANCEL_CONSTRUCTION)]
        public static void HandleGarrisonCancelConstruction(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGuid");
            packet.ReadUInt32("PlotInstanceId");
        }

        [Parser(Opcode.CMSG_GARRISON_CHECK_UPGRADEABLE)]
        public static void HandleGarrisonCheckUpgradeable(Packet packet)
        {
            packet.ReadUInt32E<GarrisonType>("GarrTypeId");
        }

        [Parser(Opcode.CMSG_GARRISON_GENERATE_RECRUITS)]
        public static void HandleGarrisonGenerateRecruits(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGuid");
            packet.ReadUInt32("TraitID");
            packet.ReadUInt32("AbilityID");
        }

        [Parser(Opcode.CMSG_GARRISON_GET_MISSION_REWARD)]
        public static void HandleGarrisonGetMissionReward(Packet packet)
        {
            packet.ReadUInt64("Unk1"); // Money ?
            packet.ReadUInt32("Unk2");
        }

        [Parser(Opcode.CMSG_GARRISON_PURCHASE_BUILDING)]
        public static void HandleGarrisonPurchaseBuilding(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGuid");
            packet.ReadUInt32("GarrPlotInstanceId");
            packet.ReadUInt32("GarrBuildingId");
        }

        [Parser(Opcode.CMSG_GARRISON_RECRUIT_FOLLOWER)]
        public static void HandleGarrisonRecruitFollower(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGuid");
            packet.ReadUInt32("FollowerID");
        }

        [Parser(Opcode.CMSG_GARRISON_GET_CLASS_SPEC_CATEGORY_INFO)]
        public static void HandleGarrisonGetClassSpecCategoryInfo(Packet packet)
        {
            packet.ReadUInt32E<GarrisonFollowerType>("GarrFollowerTypeId");
        }

        [Parser(Opcode.CMSG_GARRISON_RENAME_FOLLOWER)]
        public static void HandleGarrisonRenameFollower(Packet packet)
        {
            packet.ReadUInt64("FollowerDbId");
            var followerNewNameLength = packet.ReadBits("FollowerNewNameLength", 7);
            packet.ResetBitReader();
            packet.ReadWoWString("FollowerNewName", followerNewNameLength);
        }

        [Parser(Opcode.CMSG_GARRISON_SET_RECRUITMENT_PREFERENCES)]
        public static void HandleGarrisonSetRecruitmentPreferences(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadUInt32("AbilityID");
            packet.ReadUInt32("TraitID");
        }

        [Parser(Opcode.CMSG_GARRISON_SWAP_BUILDINGS)]
        public static void HandleGarrisonSwapBuildings(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGuid");
            packet.ReadUInt32("GarrPlotInstanceId1");
            packet.ReadUInt32("GarrPlotInstanceId2");
        }

        [Parser(Opcode.CMSG_OPEN_SHIPMENT_NPC)]
        public static void HandleOpenShipmentNpc(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGuid");
        }

        [Parser(Opcode.CMSG_OPEN_TRADESKILL_NPC)]
        public static void HandleOpenTradeskillNpc(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGuid");
        }

        [Parser(Opcode.CMSG_SET_USING_PARTY_GARRISON)]
        public static void HandleSetUsingPartyGarrison(Packet packet)
        {
            packet.ReadBit("UsePartyGarrison");
        }

        [Parser(Opcode.SMSG_GARRISON_BUILDING_REMOVED)]
        public static void HandleGarrisonBuildingRemoved(Packet packet)
        {
            packet.ReadUInt32E<GarrisonType>("GarrTypeId");
            packet.ReadUInt32E<GarrisonResult>("Result");
            packet.ReadUInt32("GarrPlotInstanceId");
            packet.ReadUInt32("GarrBuildingId");
        }

        [Parser(Opcode.SMSG_GARRISON_CREATE_RESULT)]
        public static void HandleGarrisonCreateResult(Packet packet)
        {
            packet.ReadUInt32E<GarrisonSiteLevel>("GarrSiteLevelId");
            packet.ReadUInt32E<GarrisonResult>("Result");
        }

        [Parser(Opcode.SMSG_GARRISON_PLOT_PLACED)]
        public static void HandleGarrisonPlotPlaced(Packet packet)
        {
            packet.ReadUInt32E<GarrisonType>("GarrTypeId");
            V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonPlotInfo(packet, "PlotInfo");
        }
    }
}
