using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_CRITERIA_UPDATE)]
        public static void HandleCriteriaPlayer(Packet packet)
        {
            packet.ReadInt32("Id");
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
            WowPacketParser.Messages.Submessages.AllAchievements.ReadAllAchievements602(packet, "Data");
        }
    }
}
