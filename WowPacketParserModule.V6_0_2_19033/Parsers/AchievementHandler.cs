using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE_PLAYER)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            packet.ReadInt32("Criteria ID");
            packet.ReadInt64("Counter");
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("Flags");
            packet.ReadPackedTime("Time");
            packet.ReadTime("TimeFromStart");
            packet.ReadTime("TimeFromCreate");
        }

        [Parser(Opcode.SMSG_CRITERIA_UPDATE_ACCOUNT)]
        public static void HandleCriteriaUpdateAccount(Packet packet)
        {
            packet.ReadInt32("Criteria ID");
            packet.ReadInt64("Counter");
            packet.ReadPackedGuid128("Guid");
            packet.ReadPackedTime("Time");
            packet.ReadTime("TimeFromStart");
            packet.ReadTime("TimeFromCreate");
            packet.ReadBits("Flags", 4); // some flag... & 1 -> delete
        }
    }
}
