using WowPacketParser.DBC;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class AchievementHandler
    {
        [Parser(Opcode.SMSG_BROADCAST_ACHIEVEMENT)]
        public static void HandleBroadcastAchievement(Packet packet)
        {
            var nameLength = packet.ReadBits(7);
            packet.ReadBit("GuildAchievement");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadUInt32("AchievementID");
            packet.ReadWoWString("Name", nameLength);
        }
    }
}
