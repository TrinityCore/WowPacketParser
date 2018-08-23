using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_ACHIEVEMENT_DELETED)]
        public static void HandleAchievementDeleted(Packet packet)
        {
            packet.ReadUInt32("AchievementID");
            packet.ReadUInt32("Immunities");
        }

        [Parser(Opcode.SMSG_SERVER_FIRST_ACHIEVEMENT)]
        public static void HandleServerFirstAchievement(Packet packet)
        {
            var nameLen = packet.ReadBits(7);
            packet.ReadBit("GuildAchievement");
            var guid = packet.ReadGuid("PlayerGUID");
            packet.ReadUInt32<AchievementId>("AchievementId");
            var name = packet.ReadWoWString("Name", nameLen);

            StoreGetters.AddName(guid, name);
        }
    }
}
