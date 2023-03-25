using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V10_0_0_46181.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_CHAT_CAN_LOCAL_WHISPER_TARGET_REQUEST)]
        public static void HandleChatCanLocalWhisperTargetRequest(Packet packet)
        {
            packet.ReadPackedGuid128("WhisperTarget");
        }

        [Parser(Opcode.SMSG_CHAT_CAN_LOCAL_WHISPER_TARGET_RESPONSE)]
        public static void HandleChatCanLocalWhisperTargetResponse(Packet packet)
        {
            packet.ReadPackedGuid128("WhisperTarget");
            packet.ReadInt32E<ChatWhisperTargetStatus>("Status");
        }
    }
}
