using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GarrisonHandler
    {
        private static void ReadGarrisonMission(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("DbID", indexes);
            packet.ReadInt32("MissionRecID", indexes);

            packet.ReadUInt32("OfferTime", indexes);
            packet.ReadUInt32("OfferDuration", indexes);
            packet.ReadUInt32("StartTime", indexes);
            packet.ReadUInt32("TravelDuration", indexes);
            packet.ReadUInt32("MissionDuration", indexes);

            packet.ReadInt32("MissionState", indexes);
        }

        private static void ReadGarrisonBuildingInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("GarrPlotInstanceID", indexes);
            packet.ReadInt32("GarrBuildingID", indexes);
            packet.ReadTime("TimeBuilt", indexes);
            packet.ReadInt32("CurrentGarSpecID", indexes);
            packet.ReadInt32("Unk", indexes);

            packet.ResetBitReader();

            packet.ReadBit("Active", indexes);
        }

        private static void ReadGarrisonFollower(Packet packet, params object[] indexes)
        {
            packet.ReadInt64("DbID", indexes);

            packet.ReadInt32("GarrFollowerID", indexes);
            packet.ReadInt32("Quality", indexes);
            packet.ReadInt32("FollowerLevel", indexes);
            packet.ReadInt32("ItemLevelWeapon", indexes);
            packet.ReadInt32("ItemLevelArmor", indexes);
            packet.ReadInt32("Xp", indexes);
            packet.ReadInt32("CurrentBuildingID", indexes);
            packet.ReadInt32("CurrentMissionID", indexes);
            var int40 = packet.ReadInt32("AbilityCount", indexes);
            packet.ReadInt32("UnkInt", indexes);

            var indexString = Packet.GetIndexString(indexes);
            for (int i = 0; i < int40; i++)
                packet.ReadInt32(String.Format("{0} [{1}] AbilityID", indexString, i));
        }

        private static void ReadGarrisonPlotInfo(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("GarrPlotInstanceID", indexes);
            packet.ReadVector4("PlotPos", indexes);
            packet.ReadInt32("PlotType", indexes);
        }

        private static void ReadCharacterShipment(Packet packet, params object[] indexes)
        {
            packet.ReadInt32("ShipmentRecID", indexes);
            packet.ReadInt64("ShipmentID", indexes);
            packet.ReadTime("CreationTime", indexes);
            packet.ReadInt32("ShipmentDuration", indexes);
        }

        [Parser(Opcode.CMSG_GET_GARRISON_INFO)]
        [Parser(Opcode.CMSG_GARRISON_REQUEST_LANDING_PAGE_SHIPMENT_INFO)]
        [Parser(Opcode.CMSG_GARRISON_REQUEST_BLUEPRINT_AND_SPECIALIZATION_DATA)]
        [Parser(Opcode.CMSG_GARRISON_REQUEST_UPGRADEABLE)]
        [Parser(Opcode.CMSG_GARRISON_UNK1)]
        public static void HandleGarrisonZero(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_GARRISON_MISSION_BONUS_ROLL)]
        public static void HandleGarrisonMissionBonusRoll(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadUInt32("MissionRecID");
        }

        [Parser(Opcode.SMSG_GARRISON_MISSION_BONUS_ROLL_RESULT)]
        public static void HandleGarrisonMissionBonusRollResult(Packet packet)
        {
            ReadGarrisonMission(packet);

            packet.ReadInt32("MissionRecID");
            packet.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_GARRISON_REMOTE_INFO)]
        public static void HandleGarrisonRemoteInfo(Packet packet)
        {
            var int20 = packet.ReadInt32("InfoCount");
            for (int i = 0; i < int20; i++)
            {
                packet.ReadInt32("GarrSiteLevelID", i);

                var int1 = packet.ReadInt32("BuildingsCount", i);
                for (int j = 0; j < int1; j++)
                {
                    packet.ReadInt32("GarrPlotInstanceID", i, j);
                    packet.ReadInt32("GarrBuildingID", i, j);
                }
            }
        }

        [Parser(Opcode.CMSG_GARRISON_START_MISSION)]
        public static void HandleGarrisonStartMission(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");

            var int40 = packet.ReadInt32("InfoCount");
            packet.ReadInt32("MissionRecID");

            for (int i = 0; i < int40; i++)
                packet.ReadInt64("FollowerDBIDs", i);
        }

        [Parser(Opcode.CMSG_GARRISON_COMPLETE_MISSION)]
        public static void HandleGarrisonCompleteMission(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadInt32("MissionRecID");
        }

        [Parser(Opcode.SMSG_GARRISON_UNK1)] // trigger on CMSG_GARRISON_UNK1
        public static void HandleGarrisonUnk1(Packet packet)
        {
            var int40 = packet.ReadInt32("Count");

            for (int i = 0; i < int40; i++)
            {
                packet.ReadInt32("Unk1", i);
                packet.ReadVector3("PosUnk", i);
            }
        }

        [Parser(Opcode.SMSG_GARRISON_REQUEST_BLUEPRINT_AND_SPECIALIZATION_DATA_RESULT)]
        public static void HandleGarrisonRequestBlueprintAndSpecializationDataResult(Packet packet)
        {
            var int8 = packet.ReadInt32("SpecializationsKnownCount");
            var int4 = packet.ReadInt32("BlueprintsKnownCount");

            for (var i = 0; i < int8; i++)
                packet.ReadInt32("SpecializationsKnown", i);

            for (var i = 0; i < int4; i++)
                packet.ReadInt32("BlueprintsKnown", i);
        }

        [Parser(Opcode.SMSG_GARRISON_ASSIGN_FOLLOWER_TO_BUILDING_RESULT)]
        public static void HandleGarrisonAssignFollowerToBuildingResult(Packet packet)
        {
            packet.ReadInt64("FollowerDBID");
            packet.ReadInt32("Result");
            packet.ReadInt32("PlotInstanceID");
        }

        [Parser(Opcode.SMSG_GARRISON_BUILDING_ACTIVATED)]
        public static void HandleGarrisonBuildingActivated(Packet packet)
        {
            packet.ReadInt32("GarrPlotInstanceID");
        }

        [Parser(Opcode.SMSG_GARRISON_BUILDING_REMOVED)]
        public static void HandleGarrBuildingID(Packet packet)
        {
            packet.ReadInt32("Unk1");
            packet.ReadInt32("GarrPlotInstanceID");
            packet.ReadInt32("GarrBuildingID");
        }

        [Parser(Opcode.SMSG_GARRISON_LANDINGPAGE_SHIPMENTS)]
        public static void HandleGarrisonLandingPage(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (int i = 0; i < count; i++)
            {
                packet.ReadInt32("MissionRecID", i);
                packet.ReadInt64("FollowerDBID", i);
                packet.ReadInt32("Unk1", i);
                packet.ReadInt32("Unk2", i);
            }
        }

        [Parser(Opcode.SMSG_GARRISON_ADD_MISSION_RESULT)]
        public static void HandleGarrisonAddMissionResult(Packet packet)
        {
            ReadGarrisonMission(packet);

            packet.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_GARRISON_UPGRADE_RESULT)]
        public static void HandleGarrisonUpgradeResult(Packet packet)
        {
            packet.ReadInt32("Result");
            packet.ReadInt32("GarrSiteLevelID");
        }

        [Parser(Opcode.SMSG_GARRISON_START_MISSION_RESULT)]
        public static void HandleGarrisonStartMissionResult(Packet packet)
        {
            packet.ReadInt32("Result");
            ReadGarrisonMission(packet);

            var followerCount = packet.ReadInt32("FollowerCount");
            for (int i = 0; i < followerCount; i++)
                packet.ReadInt64("FollowerDBIDs");
        }

        [Parser(Opcode.SMSG_GARRISON_UPGRADEABLE_RESULT)]
        public static void HandleClientGarrisonUpgradeableResult(Packet packet)
        {
            packet.ReadInt32("Result");
        }

        [Parser(Opcode.SMSG_DISPLAY_TOAST)]
        public static void HandleDisplayToast(Packet packet)
        {
            packet.ReadInt32("Quantity");
            packet.ReadByte("DisplayToastMethod");
            packet.ReadBit("Mailed");
            var type = packet.ReadBits("Type", 2);

            if (type == 3)
                packet.ReadBit("BonusRoll");

            if (type == 3)
            {
                ItemHandler.ReadItemInstance(packet);
                packet.ReadInt32("SpecializationID");
                packet.ReadInt32("ItemQuantity?");
            }

            if (type == 1)
                packet.ReadInt32("CurrencyID");
        }

        [Parser(Opcode.CMSG_GARRISON_OPEN_MISSION_NPC)]
        public static void HandleGarrisonOpenMissionNPC(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
        }

        [Parser(Opcode.SMSG_GET_GARRISON_INFO_RESULT)]
        public static void HandleGetGarrisonInfoResult(Packet packet)
        {
            packet.ReadInt32("GarrSiteID");
            packet.ReadInt32("GarrSiteLevelID");
            packet.ReadInt32("FactionIndex");

            var int92 = packet.ReadInt32("GarrisonBuildingInfoCount");
            var int52 = packet.ReadInt32("GarrisonPlotInfoCount");
            var int68 = packet.ReadInt32("GarrisonFollowerCount");
            var int36 = packet.ReadInt32("GarrisonMissionCount");
            var int16 = packet.ReadInt32("ArchivedMissionsCount");

            packet.ReadInt32("Unk1");

            for (int i = 0; i < int92; i++)
                ReadGarrisonBuildingInfo(packet, i);

            for (int i = 0; i < int52; i++)
                ReadGarrisonPlotInfo(packet, i);

            for (int i = 0; i < int68; i++)
                ReadGarrisonFollower(packet, i);

            for (int i = 0; i < int36; i++)
                ReadGarrisonMission(packet, i);

            for (int i = 0; i < int16; i++)
                packet.ReadInt32("ArchivedMissions", i);
        }

        [Parser(Opcode.SMSG_GARRISON_UNK2)] // GARRISON_FOLLOWER_XP_CHANGED
        public static void HandleGarrisonUnk2(Packet packet)
        {
            packet.ReadInt32("Result");
            ReadGarrisonFollower(packet);
            ReadGarrisonFollower(packet);
        }

        [Parser(Opcode.SMSG_GARRISON_COMPLETE_MISSION_RESULT)] // GARRISON_MISSION_COMPLETE_RESPONSE
        public static void HandleGarrisonCompleteMissionResult(Packet packet)
        {
            packet.ReadInt32("Result");
            ReadGarrisonMission(packet);
            packet.ReadInt32("MissionRecID");
            packet.ReadBit("Succeeded");
        }

        [Parser(Opcode.SMSG_GARRISON_UNK3)] // GARRISON_MISSION_NPC_OPENED / GARRISON_MISSION_LIST_UPDATE
        public static void HandleGarrisonUnk3(Packet packet)
        {
            packet.ReadInt32("Result");

            var count = packet.ReadInt32("MissionsCount");
            for (int i = 0; i < count; i++)
                packet.ReadInt32("Missions", i);

            packet.ReadBit("Succeeded");
        }

        [Parser(Opcode.CMSG_CREATE_SHIPMENT)]
        [Parser(Opcode.CMSG_GET_SHIPMENT_INFO)]
        public static void HandleGetShipmentInfo(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
        }

        [Parser(Opcode.SMSG_GET_SHIPMENT_INFO_RESPONSE)]
        public static void HandleGetShipmentInfoResponse(Packet packet)
        {
            packet.ReadBit("Success");

            packet.ReadInt32("ShipmentID");
            packet.ReadInt32("MaxShipments");
            var characterShipmentCount = packet.ReadInt32("CharacterShipmentCount");
            packet.ReadInt32("PlotInstanceID");

            for (int i = 0; i < characterShipmentCount; i++)
                ReadCharacterShipment(packet);
        }
    }
}
