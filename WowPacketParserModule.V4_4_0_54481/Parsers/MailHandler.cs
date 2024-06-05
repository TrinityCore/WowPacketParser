using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V4_4_0_54481.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.CMSG_QUERY_NEXT_MAIL_TIME)]
        public static void HandleNullMail(Packet packet)
        {
        }
    }
}
