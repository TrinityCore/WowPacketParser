using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class ChatHandler
    {
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_GUILD)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_OFFICER)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_PARTY)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_RAID)]
        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_INSTANCE_CHAT)]
        public static void HandleAddonMessage(Packet packet)
        {
            var prefixLen = packet.ReadBits(5);
            var testLen = packet.ReadBits(9);

            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Text", testLen);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_WHISPER)]
        public static void HandleAddonMessageWhisper(Packet packet)
        {
            var targetLen = packet.ReadBits(9);
            var prefixLen = packet.ReadBits(5);
            var testLen = packet.ReadBits(9);

            packet.ReadWoWString("Target", targetLen);
            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Text", testLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_GUILD)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_YELL)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_SAY)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_PARTY)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Text", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL)]
        public static void HandleChatAddonMessageChannel(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_0_39185))
                packet.ReadPackedGuid128("ChannelGUID");
            var channelNameLen = packet.ReadBits(9);

            var msgBitsLen = 9;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                msgBitsLen = 11;
            var msgLen = packet.ReadBits(msgBitsLen);

            packet.ReadWoWString("Target", channelNameLen);
            packet.ReadWoWString("Text", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var recvName = packet.ReadBits(9);
            var msgLen = packet.ReadBits(9);

            packet.ReadWoWString("Target", recvName);
            packet.ReadWoWString("Text", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_DND)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_AFK)]
        public static void HandleMessageChat(Packet packet)
        {
            var msgBitsLen = 9;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                msgBitsLen = 11;
            var len = packet.ReadBits(msgBitsLen);
            packet.ReadWoWString("Message", len);
        }
    }
}
