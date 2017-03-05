using System.Reflection.Emit;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GarrisonHandler
    {
        private static void ReadGarrisonMission(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt64("DbID", indexes);
            packet.Translator.ReadInt32("MissionRecID", indexes);

            packet.Translator.ReadTime("OfferTime", indexes);
            packet.Translator.ReadUInt32("OfferDuration", indexes);
            packet.Translator.ReadTime("StartTime", indexes);
            packet.Translator.ReadUInt32("TravelDuration", indexes);
            packet.Translator.ReadUInt32("MissionDuration", indexes);

            packet.Translator.ReadInt32("MissionState", indexes);
        }

        private static void ReadGarrisonBuildingInfo(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("GarrPlotInstanceID", indexes);
            packet.Translator.ReadInt32("GarrBuildingID", indexes);
            packet.Translator.ReadTime("TimeBuilt", indexes);
            packet.Translator.ReadInt32("CurrentGarSpecID", indexes);
            packet.Translator.ReadTime("TimeSpecCooldown", indexes);

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("Active", indexes);
        }

        private static void ReadGarrisonFollower(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt64("DbID", indexes);

            packet.Translator.ReadInt32("GarrFollowerID", indexes);
            packet.Translator.ReadInt32("Quality", indexes);
            packet.Translator.ReadInt32("FollowerLevel", indexes);
            packet.Translator.ReadInt32("ItemLevelWeapon", indexes);
            packet.Translator.ReadInt32("ItemLevelArmor", indexes);
            packet.Translator.ReadInt32("Xp", indexes);
            packet.Translator.ReadInt32("CurrentBuildingID", indexes);
            packet.Translator.ReadInt32("CurrentMissionID", indexes);
            var int40 = packet.Translator.ReadInt32("AbilityCount", indexes);
            packet.Translator.ReadInt32("FollowerStatus", indexes);

            for (int i = 0; i < int40; i++)
                packet.Translator.ReadInt32("AbilityID", indexes, i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
            {
                packet.Translator.ResetBitReader();

                var len = packet.Translator.ReadBits(7);
                packet.Translator.ReadWoWString("CustomName", len, indexes);
            }
        }

        public static void ReadGarrisonPlotInfo(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("GarrPlotInstanceID", indexes);
            packet.Translator.ReadVector4("PlotPos", indexes);
            packet.Translator.ReadInt32("PlotType", indexes);
        }

        private static void ReadCharacterShipment60x(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("ShipmentRecID", indexes);
            packet.Translator.ReadInt64("ShipmentID", indexes);
            packet.Translator.ReadTime("CreationTime", indexes);
            packet.Translator.ReadInt32("ShipmentDuration", indexes);
        }

        private static void ReadCharacterShipment61x(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("ShipmentRecID", indexes);
            packet.Translator.ReadInt64("ShipmentID", indexes);
            packet.Translator.ReadInt64("Unk2", indexes);
            packet.Translator.ReadTime("CreationTime", indexes);
            packet.Translator.ReadInt32("ShipmentDuration", indexes);
            packet.Translator.ReadInt32("Unk8", indexes);
        }
        public static void ReadGarrisonMissionAreaBonus(Packet packet, params object[] indexes)
        {
            packet.Translator.ReadInt32("GarrMssnBonusAbilityID", indexes);
            packet.Translator.ReadInt32("StartTime", indexes);
        }

        [Parser(Opcode.CMSG_GET_GARRISON_INFO)]
        [Parser(Opcode.CMSG_GARRISON_REQUEST_LANDING_PAGE_SHIPMENT_INFO)]
        [Parser(Opcode.CMSG_GARRISON_REQUEST_BLUEPRINT_AND_SPECIALIZATION_DATA)]
        [Parser(Opcode.CMSG_GARRISON_CHECK_UPGRADEABLE)]
        [Parser(Opcode.CMSG_GARRISON_UNK1)]
        [Parser(Opcode.CMSG_GARRISON_GET_BUILDING_LANDMARKS)]
        public static void HandleGarrisonZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_GARRISON_MISSION_BONUS_ROLL)]
        public static void HandleGarrisonMissionBonusRoll(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
            packet.Translator.ReadUInt32("MissionRecID");
        }

        [Parser(Opcode.SMSG_GARRISON_MISSION_BONUS_ROLL_RESULT)]
        public static void HandleGarrisonMissionBonusRollResult(Packet packet)
        {
            ReadGarrisonMission(packet);

            packet.Translator.ReadInt32("MissionRecID");
            packet.Translator.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_GARRISON_REMOTE_INFO, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleGarrisonRemoteInfo(Packet packet)
        {
            var int20 = packet.Translator.ReadInt32("InfoCount");
            for (int i = 0; i < int20; i++)
            {
                packet.Translator.ReadInt32("GarrSiteLevelID", i);

                var int1 = packet.Translator.ReadInt32("BuildingsCount", i);
                for (int j = 0; j < int1; j++)
                {
                    packet.Translator.ReadInt32("GarrPlotInstanceID", i, j);
                    packet.Translator.ReadInt32("GarrBuildingID", i, j);
                }
            }
        }

        [Parser(Opcode.CMSG_GARRISON_START_MISSION)]
        public static void HandleGarrisonStartMission(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");

            var int40 = packet.Translator.ReadInt32("InfoCount");
            packet.Translator.ReadInt32("MissionRecID");

            for (int i = 0; i < int40; i++)
                packet.Translator.ReadInt64("FollowerDBIDs", i);
        }

        [Parser(Opcode.CMSG_GARRISON_COMPLETE_MISSION)]
        public static void HandleGarrisonCompleteMission(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
            packet.Translator.ReadInt32("MissionRecID");
        }

        [Parser(Opcode.SMSG_GARRISON_UNK1)] // trigger on CMSG_GARRISON_UNK1
        public static void HandleGarrisonUnk1(Packet packet)
        {
            var int40 = packet.Translator.ReadInt32("Count");

            for (int i = 0; i < int40; i++)
            {
                packet.Translator.ReadInt32("Unk1", i);
                packet.Translator.ReadVector3("PosUnk", i);
            }
        }

        [Parser(Opcode.SMSG_GARRISON_REQUEST_BLUEPRINT_AND_SPECIALIZATION_DATA_RESULT)]
        public static void HandleGarrisonRequestBlueprintAndSpecializationDataResult(Packet packet)
        {
            var int8 = packet.Translator.ReadInt32("SpecializationsKnownCount");
            var int4 = packet.Translator.ReadInt32("BlueprintsKnownCount");

            for (var i = 0; i < int8; i++)
                packet.Translator.ReadInt32("SpecializationsKnown", i);

            for (var i = 0; i < int4; i++)
                packet.Translator.ReadInt32("BlueprintsKnown", i);
        }

        [Parser(Opcode.SMSG_GARRISON_ASSIGN_FOLLOWER_TO_BUILDING_RESULT)]
        public static void HandleGarrisonAssignFollowerToBuildingResult(Packet packet)
        {
            packet.Translator.ReadInt64("FollowerDBID");
            packet.Translator.ReadInt32("Result");
            packet.Translator.ReadInt32("PlotInstanceID");
        }

        [Parser(Opcode.SMSG_GARRISON_BUILDING_ACTIVATED)]
        public static void HandleGarrisonBuildingActivated(Packet packet)
        {
            packet.Translator.ReadInt32("GarrPlotInstanceID");
        }

        [Parser(Opcode.SMSG_GARRISON_BUILDING_REMOVED)]
        public static void HandleGarrBuildingID(Packet packet)
        {
            packet.Translator.ReadInt32("Unk1");
            packet.Translator.ReadInt32("GarrPlotInstanceID");
            packet.Translator.ReadInt32("GarrBuildingID");
        }

        [Parser(Opcode.SMSG_GARRISON_LANDING_PAGE_SHIPMENT_INFO)]
        public static void HandleGarrisonLandingPage(Packet packet)
        {
            var count = packet.Translator.ReadInt32("Count");
            for (int i = 0; i < count; i++)
            {
                packet.Translator.ReadInt32("MissionRecID", i);
                packet.Translator.ReadInt64("FollowerDBID", i);
                packet.Translator.ReadInt32("Unk1", i);
                packet.Translator.ReadInt32("Unk2", i);
            }
        }

        [Parser(Opcode.SMSG_GARRISON_ADD_MISSION_RESULT)]
        public static void HandleGarrisonAddMissionResult(Packet packet)
        {
            ReadGarrisonMission(packet);

            packet.Translator.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_GARRISON_UPGRADE_RESULT)]
        public static void HandleGarrisonUpgradeResult(Packet packet)
        {
            packet.Translator.ReadInt32("Result");
            packet.Translator.ReadInt32("GarrSiteLevelID");
        }

        [Parser(Opcode.SMSG_GARRISON_START_MISSION_RESULT)]
        public static void HandleGarrisonStartMissionResult(Packet packet)
        {
            packet.Translator.ReadInt32("Result");
            ReadGarrisonMission(packet);

            var followerCount = packet.Translator.ReadInt32("FollowerCount");
            for (int i = 0; i < followerCount; i++)
                packet.Translator.ReadInt64("FollowerDBIDs");
        }

        [Parser(Opcode.SMSG_GARRISON_UPGRADEABLE_RESULT)]
        [Parser(Opcode.SMSG_GARRISON_IS_UPGRADEABLE_RESULT)]
        public static void HandleClientGarrisonUpgradeableResult(Packet packet)
        {
            packet.Translator.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_DISPLAY_TOAST, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleDisplayToast600(Packet packet)
        {
            packet.Translator.ReadInt32("Quantity");
            packet.Translator.ReadByte("DisplayToastMethod");
            packet.Translator.ReadBit("Mailed");
            var type = packet.Translator.ReadBits("Type", 2);

            if (type == 3)
            {
                packet.Translator.ReadBit("BonusRoll");
                ItemHandler.ReadItemInstance(packet);
                packet.Translator.ReadInt32("SpecializationID");
                packet.Translator.ReadInt32("ItemQuantity?");
            }

            if (type == 1)
                packet.Translator.ReadInt32("CurrencyID");
        }

        [Parser(Opcode.SMSG_DISPLAY_TOAST, ClientVersionBuild.V6_1_0_19678, ClientVersionBuild.V6_2_0_20173)]
        public static void HandleDisplayToast610(Packet packet)
        {
            packet.Translator.ReadInt32("Quantity");
            packet.Translator.ReadByte("DisplayToastMethod");
            packet.Translator.ReadBit("Mailed");
            var type = packet.Translator.ReadBits("Type", 2);

            if (type == 0)
            {
                packet.Translator.ReadBit("BonusRoll");
                ItemHandler.ReadItemInstance(packet);
                packet.Translator.ReadInt32("SpecializationID");
                packet.Translator.ReadInt32("ItemQuantity?");
            }

            if (type == 2)
                packet.Translator.ReadInt32("CurrencyID");
        }

        [Parser(Opcode.SMSG_DISPLAY_TOAST, ClientVersionBuild.V6_2_0_20173, ClientVersionBuild.V6_2_2_20444)]
        public static void HandleDisplayToast620(Packet packet)
        {
            packet.Translator.ReadInt32("Quantity");
            packet.Translator.ReadByte("DisplayToastMethod");
            packet.Translator.ReadBit("Mailed");
            var type = packet.Translator.ReadBits("Type", 2);

            if (type == 3)
            {
                packet.Translator.ReadBit("BonusRoll");
                ItemHandler.ReadItemInstance(packet);
                packet.Translator.ReadInt32("SpecializationID");
                packet.Translator.ReadInt32("ItemQuantity?");
            }

            if (type == 1)
                packet.Translator.ReadInt32("CurrencyID");
        }

        [Parser(Opcode.SMSG_DISPLAY_TOAST, ClientVersionBuild.V6_2_2_20444, ClientVersionBuild.V6_2_3_20726)]
        public static void HandleDisplayToast622(Packet packet)
        {
            packet.Translator.ReadInt32("Quantity");
            packet.Translator.ReadByte("DisplayToastMethod");
            packet.Translator.ReadBit("Mailed");
            var type = packet.Translator.ReadBits("Type", 2);

            if (type == 2)
            {
                packet.Translator.ReadBit("BonusRoll");
                ItemHandler.ReadItemInstance(packet);
                packet.Translator.ReadInt32("SpecializationID");
                packet.Translator.ReadInt32("ItemQuantity?");
            }

            if (type == 1)
                packet.Translator.ReadInt32("CurrencyID");
        }

        [Parser(Opcode.SMSG_DISPLAY_TOAST, ClientVersionBuild.V6_2_3_20726, ClientVersionBuild.V6_2_4_21315)]
        public static void HandleDisplayToast623(Packet packet)
        {
            packet.Translator.ReadInt32("Quantity");
            packet.Translator.ReadByte("DisplayToastMethod");
            packet.Translator.ReadBit("Mailed");
            var type = packet.Translator.ReadBits("Type", 2);

            if (type == 2)
            {
                packet.Translator.ReadBit("BonusRoll");
                ItemHandler.ReadItemInstance(packet);
                packet.Translator.ReadInt32("SpecializationID");
                packet.Translator.ReadInt32("ItemQuantity?");
            }

            if (type == 1)
                packet.Translator.ReadInt32("CurrencyID");
        }

        [Parser(Opcode.SMSG_DISPLAY_TOAST, ClientVersionBuild.V6_2_4_21315)]
        public static void HandleDisplayToast624(Packet packet)
        {
            packet.Translator.ReadInt32("Quantity");
            packet.Translator.ReadByte("DisplayToastMethod");
            packet.Translator.ReadBit("Mailed");
            var type = packet.Translator.ReadBits("Type", 2);

            if (type == 0)
            {
                packet.Translator.ReadBit("BonusRoll");
                ItemHandler.ReadItemInstance(packet);
                packet.Translator.ReadInt32("SpecializationID");
                packet.Translator.ReadInt32("ItemQuantity?");
            }

            if (type == 1)
                packet.Translator.ReadInt32("CurrencyID");
        }

        [Parser(Opcode.SMSG_GET_GARRISON_INFO_RESULT)]
        public static void HandleGetGarrisonInfoResult(Packet packet)
        {
            packet.Translator.ReadInt32("GarrSiteID");
            packet.Translator.ReadInt32("GarrSiteLevelID");
            packet.Translator.ReadInt32("FactionIndex");

            var int92 = packet.Translator.ReadInt32("GarrisonBuildingInfoCount");
            var int52 = packet.Translator.ReadInt32("GarrisonPlotInfoCount");
            var int68 = packet.Translator.ReadInt32("GarrisonFollowerCount");
            var int36 = packet.Translator.ReadInt32("GarrisonMissionCount");
            var areaBonusCount = ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? packet.Translator.ReadInt32("GarrisonMissionAreaBonusCount") : 0;
            var canStartMissionCount = ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? packet.Translator.ReadInt32("CanStartMission") : 0;
            var int16 = packet.Translator.ReadInt32("ArchivedMissionsCount");

            packet.Translator.ReadInt32("NumFollowerActivationsRemaining");

            for (int i = 0; i < int92; i++)
                ReadGarrisonBuildingInfo(packet, "GarrisonBuildingInfo", i);

            for (int i = 0; i < int52; i++)
                ReadGarrisonPlotInfo(packet, "GarrisonPlotInfo", i);

            for (int i = 0; i < int68; i++)
                ReadGarrisonFollower(packet, "GarrisonFollower", i);

            for (int i = 0; i < int36; i++)
                ReadGarrisonMission(packet, "GarrisonMission", i);

            for (int i = 0; i < areaBonusCount; i++)
                ReadGarrisonMissionAreaBonus(packet, "GarrisonMissionAreaBonus", i);

            for (int i = 0; i < int16; i++)
                packet.Translator.ReadInt32("ArchivedMissions", i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                packet.Translator.ResetBitReader();

            for (int i = 0; i < canStartMissionCount; i++)
                packet.Translator.ReadBit("CanStartMission", i);
        }

        [Parser(Opcode.SMSG_GARRISON_FOLLOWER_CHANGED_XP)] // GARRISON_FOLLOWER_XP_CHANGED
        public static void HandleGarrisonUnk2(Packet packet)
        {
            packet.Translator.ReadInt32("Result");
            ReadGarrisonFollower(packet);
            ReadGarrisonFollower(packet);
        }

        [Parser(Opcode.SMSG_GARRISON_COMPLETE_MISSION_RESULT)] // GARRISON_MISSION_COMPLETE_RESPONSE
        public static void HandleGarrisonCompleteMissionResult(Packet packet)
        {
            packet.Translator.ReadInt32("Result");
            ReadGarrisonMission(packet);
            packet.Translator.ReadInt32("MissionRecID");
            packet.Translator.ReadBit("Succeeded");
        }

        [Parser(Opcode.SMSG_GARRISON_UNK3)] // GARRISON_MISSION_NPC_OPENED / GARRISON_MISSION_LIST_UPDATE
        public static void HandleGarrisonUnk3(Packet packet)
        {
            packet.Translator.ReadInt32("Result");

            var count = packet.Translator.ReadInt32("MissionsCount");
            for (int i = 0; i < count; i++)
                packet.Translator.ReadInt32("Missions", i);

            packet.Translator.ReadBit("Succeeded");
        }

        [Parser(Opcode.CMSG_CREATE_SHIPMENT, ClientVersionBuild.V6_0_2_19033, ClientVersionBuild.V6_1_0_19678)]
        [Parser(Opcode.CMSG_GET_SHIPMENT_INFO)]
        [Parser(Opcode.CMSG_OPEN_MISSION_NPC)]
        [Parser(Opcode.CMSG_GARRISON_REQUEST_SHIPMENT_INFO)]
        public static void HandleGarrisonNpcGUID(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
        }

        [Parser(Opcode.CMSG_CREATE_SHIPMENT, ClientVersionBuild.V6_1_0_19678)]
        public static void HandleCreateShipment61x(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
            packet.Translator.ReadUInt32("Unk4");
        }

        [Parser(Opcode.CMSG_COMPLETE_ALL_READY_SHIPMENTS)]
        public static void HandleGarrisonGameObjGUID(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GameObjectGUID");
        }

        [Parser(Opcode.SMSG_GET_SHIPMENT_INFO_RESPONSE)]
        public static void HandleGetShipmentInfoResponse(Packet packet)
        {
            packet.Translator.ReadBit("Success");

            packet.Translator.ReadInt32("ShipmentID");
            packet.Translator.ReadInt32("MaxShipments");
            var characterShipmentCount = packet.Translator.ReadInt32("CharacterShipmentCount");
            packet.Translator.ReadInt32("PlotInstanceID");

            for (int i = 0; i < characterShipmentCount; i++)
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_0_19678))
                    ReadCharacterShipment61x(packet, i);
                else
                    ReadCharacterShipment60x(packet, i);
        }

        [Parser(Opcode.SMSG_CREATE_SHIPMENT_RESPONSE)]
        public static void HandleCreateShipmentResponse(Packet packet)
        {
            packet.Translator.ReadInt64("ShipmentID");
            packet.Translator.ReadUInt32("ShipmentRecID");
            packet.Translator.ReadUInt32("Result");
        }

        [Parser(Opcode.SMSG_OPEN_SHIPMENT_NPC_FROM_GOSSIP)]
        public static void HandleOpenShipmentNPCFromGossip(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
            packet.Translator.ReadUInt32("CharShipmentContainerID");
        }

        [Parser(Opcode.SMSG_GARRISON_UPGRADE_FOLLOWER_ITEM_LEVEL)]
        public static void HandleGarrisonUpgradeFollowerItemLevel(Packet packet)
        {
            ReadGarrisonFollower(packet);
        }

        [Parser(Opcode.CMSG_OPEN_TRADESKILL_NPC)]
        public static void HandleGarrisonOpenTradeskillNpc(Packet packet)
        {
            packet.Translator.ReadInt32("Unk"); // maybe: SkillLineID?
        }

        [Parser(Opcode.SMSG_GARRISON_OPEN_TRADESKILL_NPC_RESPONSE)]
        public static void HandleGarrisonOpenTradeskillNpcResponse(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GUID");

            packet.Translator.ReadInt32<SpellId>("SpellID");

            var int4 = packet.Translator.ReadInt32("SkillLineCount");
            var int20 = packet.Translator.ReadInt32("SkillRankCount");
            var int36 = packet.Translator.ReadInt32("SkillMaxRankCount");
            var int52 = packet.Translator.ReadInt32("KnownAbilitySpellCount");

            for (int i = 0; i < int4; i++)
                packet.Translator.ReadInt32("SkillLineIDs", i);

            for (int i = 0; i < int20; i++)
                packet.Translator.ReadInt32("SkillRanks", i);

            for (int i = 0; i < int36; i++)
                packet.Translator.ReadInt32("SkillMaxRanks", i);

            for (int i = 0; i < int52; i++)
                packet.Translator.ReadInt32("KnownAbilitySpellIDs", i);

            var int84 = packet.Translator.ReadInt32("PlayerConditionCount");
            for (int i = 0; i < int84; i++)
                packet.Translator.ReadInt32("PlayerConditionID", i);
        }

        [Parser(Opcode.SMSG_GET_DISPLAYED_TROPHY_LIST_RESPONSE)]
        public static void HandleGarrisonSetupTrophy(Packet packet)
        {
            var count = packet.Translator.ReadInt32("TrophyCount");
            for (int i = 0; i < count; i++) // @To-Do: need verification
            {
                packet.Translator.ReadInt32("Unk1", i);
                packet.Translator.ReadInt32("Unk2", i);
            }
        }

        [Parser(Opcode.SMSG_GARRISON_ADD_FOLLOWER_RESULT)]
        public static void HandleGarrisonAddFollowerResult(Packet packet)
        {
            packet.Translator.ReadInt32("Result");
            ReadGarrisonFollower(packet);
        }

        [Parser(Opcode.SMSG_GARRISON_PLOT_REMOVED)]
        public static void HandleGarrisonPlotRemoved(Packet packet)
        {
            packet.Translator.ReadInt32("GarrPlotInstanceID");
        }

        [Parser(Opcode.SMSG_GARRISON_SET_NUM_FOLLOWER_ACTIVATIONS_REMAINING)]
        public static void HandleGarrisonSetNumFollowerActivationsRemaining(Packet packet)
        {
            packet.Translator.ReadInt32("Activated");
        }

        [Parser(Opcode.CMSG_UPGRADE_GARRISON)]
        public static void HandleUpgradeGarrison(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
        }

        [Parser(Opcode.CMSG_REPLACE_TROPHY)]
        [Parser(Opcode.CMSG_CHANGE_MONUMENT_APPEARANCE)]
        public static void HandleReplaceTrophy(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TrophyGUID");
            packet.Translator.ReadInt32("NewTrophyID");
        }

        [Parser(Opcode.CMSG_REVERT_MONUMENT_APPEARANCE)]
        public static void HandleRevertTrophy(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("TrophyGUID");
        }

        [Parser(Opcode.CMSG_TROPHY_MONUMENT_LOAD_SELECTED_TROPHY_ID)]
        public static void HandleGetSelectedTrophyId(Packet packet)
        {
            packet.Translator.ReadInt32("TrophyID");
        }

        [Parser(Opcode.CMSG_GET_TROPHY_LIST)]
        public static void HandleGetTrophyList(Packet packet)
        {
            packet.Translator.ReadInt32("TrophyTypeID");
        }

        [Parser(Opcode.CMSG_GARRISON_SET_FOLLOWER_INACTIVE)]
        public static void HandleGarrisonSetFollowerInactive(Packet packet)
        {
            packet.Translator.ReadInt64("FollowerDBID");
            packet.Translator.ReadBit("Inactive");
        }

        [Parser(Opcode.CMSG_GARRISON_REMOVE_FOLLOWER_FROM_BUILDING)]
        public static void HandleGarrisonRemoveFollowerFromBuilding(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
            packet.Translator.ReadInt64("FollowerDBID");
        }

        [Parser(Opcode.CMSG_GARRISON_PURCHASE_BUILDING)]
        public static void HandleGarrisonPurchaseBuilding(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
            packet.Translator.ReadInt32("PlotInstanceID");
            packet.Translator.ReadInt32("BuildingID");
        }

        [Parser(Opcode.CMSG_GARRISON_ASSIGN_FOLLOWER_TO_BUILDING)]
        public static void HandleGarrisonAssignFollowerToBuilding(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("NpcGUID");
            packet.Translator.ReadInt32("PlotInstanceID");
            packet.Translator.ReadInt64("FollowerDBID");
        }

        [Parser(Opcode.SMSG_GET_TROPHY_LIST_RESPONSE)]
        public static void HandleGetTrophyListResponse(Packet packet)
        {
            packet.Translator.ReadBit("Success");
            var trophyCount = packet.Translator.ReadInt32("TrophyCount");
            for (int i = 0; i < trophyCount; i++)
            {
                packet.Translator.ReadInt32("TrophyID", i);
                packet.Translator.ReadInt32("Unk1", i);
                packet.Translator.ReadInt32("Unk2", i);
            }
        }

        [Parser(Opcode.SMSG_REPLACE_TROPHY_RESPONSE)]
        public static void HandleReplaceTrophyResponse(Packet packet)
        {
            packet.Translator.ReadBit("Success");
        }

        [Parser(Opcode.SMSG_GARRISON_MONUMENT_SELECTED_TROPHY_ID_LOADED)]
        public static void HandleGarrisonMonumentSelectedTrophyIdLoaded(Packet packet)
        {
            packet.Translator.ReadBit("Success");
            packet.Translator.ReadInt32("TrophyID");
        }

        [Parser(Opcode.SMSG_GARRISON_LEARN_BLUEPRINT_RESULT)]
        public static void HandleGarrisonLearnBlueprintResult(Packet packet)
        {
            packet.Translator.ReadInt32("Result");
            packet.Translator.ReadInt32("BuildingID");
        }

        [Parser(Opcode.SMSG_GARRISON_PLACE_BUILDING_RESULT)]
        public static void HandleGarrisonPlaceBuildingResult(Packet packet)
        {
            packet.Translator.ReadInt32("Result");
            ReadGarrisonBuildingInfo(packet, "BuildingInfo");

            packet.Translator.ResetBitReader();

            packet.Translator.ReadBit("UnkBit");
        }

        [Parser(Opcode.SMSG_GARRISON_CREATE_RESULT)]
        public static void HandleGarrisonCreateResult(Packet packet)
        {
            packet.Translator.ReadInt32("Result");
            packet.Translator.ReadInt32("GarrSiteID");
        }

        [Parser(Opcode.SMSG_GARRISON_BUILDING_LANDMARKS)]
        public static void HandleGarrisonBuildingLandmarks(Packet packet)
        {
            var count = packet.Translator.ReadInt32();
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadUInt32("GarrBuildingPlotInstID", i);
                packet.Translator.ReadVector3("Pos", i);
            }
        }

        [Parser(Opcode.SMSG_GARRISON_REMOVE_FOLLOWER_RESULT)]
        public static void HandleGarrisonRemoveFollowerResult(Packet packet)
        {
            packet.Translator.ReadInt64("FollowerDBID");
            packet.Translator.ReadInt32("Result");
            packet.Translator.ReadInt32("Destroyed");
        }
    }
}
