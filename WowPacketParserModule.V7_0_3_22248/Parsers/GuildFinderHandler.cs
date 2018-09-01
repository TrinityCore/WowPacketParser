using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class GuildFinderHandler
    {
        [Parser(Opcode.CMSG_LF_GUILD_ADD_RECRUIT)]
        public static void HandleLFGuildAddRecruit(Packet packet)
        {
            packet.ReadPackedGuid128("GuildGUID");
            packet.ReadInt32E<GuildFinderOptionsInterest>("Playstyle");
            packet.ReadInt32E<GuildFinderOptionsAvailability>("Availability");
            packet.ReadInt32E<GuildFinderOptionsRoles>("ClassRoles");
            packet.ResetBitReader();
            var len = packet.ReadBits(10);
            packet.ReadWoWString("Comment", len);
        }

        [Parser(Opcode.CMSG_LF_GUILD_BROWSE)]
        public static void HandleGuildFinderBrowse(Packet packet)
        {
            packet.ReadInt32E<GuildFinderOptionsInterest>("Guild Interests");
            packet.ReadInt32E<GuildFinderOptionsAvailability>("Availability");
            packet.ReadInt32E<GuildFinderOptionsRoles>("Class Roles");
            packet.ReadInt32("CharacterLevel");
        }

    }
}
