using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GarrisonHandler
    {
        [Parser(Opcode.CMSG_GARRISON_MISSION_BONUS_ROLL)]
        public static void HandleGarrisonMissionBonusRoll(Packet packet)
        {
            packet.ReadPackedGuid128("NpcGUID");
            packet.ReadUInt32("MissionRecID");
        }
    }
}
