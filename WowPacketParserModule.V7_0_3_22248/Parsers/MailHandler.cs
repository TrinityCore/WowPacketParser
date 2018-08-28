using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.CMSG_MAIL_GET_LIST)]
        public static void HandleGetMailList(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
        }
    }
}
