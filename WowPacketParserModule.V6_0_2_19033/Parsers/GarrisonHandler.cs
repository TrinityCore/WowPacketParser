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
    }
}
