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

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER, ClientVersionBuild.V10_2_7_54577)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            packet.ReadPackedGuid128("TargetGUID");
            packet.ReadUInt32("TargetVirtualRealmAddress");
            var targetLen = packet.ReadBits(6);
            var textLen = packet.ReadBits(11);

            if (targetLen > 1)
            {
                packet.ReadWoWString("Target", targetLen - 1);
                packet.ReadByte(); // null terminator
            }

            if (textLen > 1)
            {
                packet.ReadWoWString("Text", textLen - 1);
                packet.ReadByte(); // null terminator
            }
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_TARGETED, ClientVersionBuild.V10_2_7_54577)]
        public static void HandleChatAddonMessageTargeted(Packet packet)
        {
            V8_0_1_27101.Parsers.ChatHandler.ReadChatAddonMessageParams(packet, "Params");

            packet.ReadPackedGuid128("ChannelGUID");
            packet.ReadPackedGuid128("PlayerGUID");
            packet.ReadUInt32("PlayerVirtualRealmAddress");

            var playerNameLen = packet.ReadBits(6);
            var channelNameLen = packet.ReadBits(6);

            if (playerNameLen > 1)
            {
                packet.ReadWoWString("PlayerName", playerNameLen - 1);
                packet.ReadByte(); // null terminator
            }

            if (channelNameLen > 1)
            {
                packet.ReadWoWString("ChannelName", channelNameLen - 1);
                packet.ReadByte(); // null terminator
            }
        }
    }
}
