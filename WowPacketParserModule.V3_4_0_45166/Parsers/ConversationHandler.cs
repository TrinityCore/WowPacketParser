using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V3_4_0_45166.Parsers
{
    public static class ConversationHandler
    {
        [Parser(Opcode.CMSG_CONVERSATION_LINE_STARTED, ClientVersionBuild.V3_4_4_59817)]
        public static void HandleConversationLineStarted(Packet packet)
        {
            packet.ReadPackedGuid128("ConversationGUID");
            packet.ReadUInt32("ConversationLineID");
        }
    }
}
