using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class ConversationHandler
    {
        [Parser(Opcode.CMSG_CONVERSATION_LINE_STARTED)]
        public static void HandleConversationLineStarted(Packet packet)
        {
            packet.ReadPackedGuid128("ConversationGUID");
            packet.ReadUInt32("ConversationLineID");
        }
    }
}
