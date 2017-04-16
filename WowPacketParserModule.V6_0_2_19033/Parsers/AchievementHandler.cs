using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            packet.ReadInt32<CriteriaId>("Id");
            packet.ReadInt64("Quantity");
            packet.ReadPackedGuid128("Guid");
            packet.ReadInt32("Flags");
            packet.ReadPackedTime("Date");
            packet.ReadTime("TimeFromStart");
            packet.ReadTime("TimeFromCreate");
        }

        [Parser(Opcode.SMSG_RESPOND_INSPECT_ACHIEVEMENTS)]
        public static void HandleRespondInspectAchievements(Packet packet)
        {
            packet.ReadPackedGuid128("Player");
            AllAchievements.Read6(packet, "Data");
        }
    }
}
