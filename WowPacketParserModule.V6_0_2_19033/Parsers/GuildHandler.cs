using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_QUERY)]
        public static void HandleGuildQuery(Packet packet)
        {
            packet.ReadPackedGuid128("Guild Guid");
            packet.ReadPackedGuid128("Player Guid");
        }
    }
}
