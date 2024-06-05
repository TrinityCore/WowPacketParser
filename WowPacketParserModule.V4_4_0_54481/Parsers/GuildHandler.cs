using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class GuildHandler
    {
        [Parser(Opcode.CMSG_GUILD_BANK_REMAINING_WITHDRAW_MONEY_QUERY)]
        public static void HandleGuildZero(Packet packet)
        {
        }
    }
}
