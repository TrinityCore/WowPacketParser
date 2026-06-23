using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

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

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER, ClientVersionBuild.V10_2_7_54577)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadUInt32("TargetVirtualRealmAddress");
            var targetLen = packet.ReadBits(9);
            var textLen = packet.ReadBits(11);

            packet.ReadDynamicString("Target", targetLen);
            packet.ReadDynamicString("Text", textLen);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_TARGETED, ClientVersionBuild.V10_2_7_54577)]
        public static void HandleChatAddonMessageTargeted(Packet packet)
        {
            V8_0_1_27101.Parsers.ChatHandler.ReadChatAddonMessageParams(packet, "Params");

            packet.ReadPackedGuid128("ChannelGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadUInt32("PlayerVirtualRealmAddress");

            var playerNameLen = packet.ReadBits(9);
            var channelNameLen = packet.ReadBits(8);

            packet.ReadDynamicString("PlayerName", playerNameLen);
            packet.ReadDynamicString("ChannelName", channelNameLen);
        }
    }
}
