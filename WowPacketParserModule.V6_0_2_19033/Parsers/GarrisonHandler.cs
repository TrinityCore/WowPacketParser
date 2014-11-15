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
    }
}
