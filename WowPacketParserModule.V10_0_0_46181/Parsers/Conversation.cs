using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class ConversationHandler
    {
        [Parser(Opcode.CMSG_CONVERSATION_CINEMATIC_READY)]
        public static void HandleConversationCinematicReady(Packet packet)
        {
            packet.ReadPackedGuid128("ConversationID");
        }
    }
}
