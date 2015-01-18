using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GarrisonHandler
    {
        private static void ReadGarrisonMission(ref Packet packet)
        {
            packet.ReadInt64("DbID");
            packet.ReadInt32("MissionRecID");

            packet.ReadTime("OfferTime");
            packet.ReadTime("OfferDuration");
            packet.ReadTime("StartTime");
            packet.ReadTime("TravelDuration");
            packet.ReadTime("MissionDuration");

            packet.ReadInt32("MissionState");
        }

        [Parser(Opcode.CMSG_GET_GARRISON_INFO)]
        [Parser(Opcode.CMSG_GARRISON_REQUEST_LANDING_PAGE_SHIPMENT_INFO)]
        [Parser(Opcode.CMSG_GARRISON_REQUEST_BLUEPRINT_AND_SPECIALIZATION_DATA)]
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

        [Parser(Opcode.SMSG_GARRISON_COMPLETE_MISSION_RESULT)]
        public static void HandleGarrisonCompleteMissionResult(Packet packet)
        {
            ReadGarrisonMission(ref packet);

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
            ReadGarrisonMission(ref packet);

            packet.ReadInt32("Result");
        }
    }
}
