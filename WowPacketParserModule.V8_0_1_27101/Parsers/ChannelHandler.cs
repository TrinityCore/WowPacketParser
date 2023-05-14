using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_JOINED, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleChannelNotifyJoined(Packet packet)
        {
            var channelLen = packet.ReadBits(8);
            uint channelWelcomeMsgLen = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_0_30898))
                channelWelcomeMsgLen = packet.ReadBits(11);
            else
                channelWelcomeMsgLen = packet.ReadBits(10);

            packet.ReadUInt32E<ChannelFlag>("ChannelFlags");
            packet.ReadInt32("ChatChannelID");
            packet.ReadUInt64("InstanceID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_2_5_31921))
                packet.ReadPackedGuid128("ChannelGUID");

            packet.ReadWoWString("Channel", channelLen);
            packet.ReadWoWString("ChannelWelcomeMsg", channelWelcomeMsgLen);
        }

        [Parser(Opcode.SMSG_CHANNEL_NOTIFY_LEFT, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleChannelNotifyLeft(Packet packet)
        {
            var bits20 = packet.ReadBits(8);
            packet.ReadBit("Suspended");
            packet.ReadInt32("ChatChannelID");
            packet.ReadWoWString("Channel", bits20);
        }

        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleChannelJoin(Packet packet)
        {
            packet.ReadInt32("ChatChannelId");
            packet.ReadBit("CreateVoiceSession");
            packet.ReadBit("Internal");

            var channelLength = packet.ReadBits(8);
            var passwordLength = packet.ReadBits(8);

            packet.ResetBitReader();

            packet.ReadWoWString("ChannelName", channelLength);
            packet.ReadWoWString("Password", passwordLength);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_GUILD, ClientVersionBuild.V8_1_0_28724)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_YELL, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleClientChatMessage(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(10);
            packet.ReadWoWString("Text", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_SAY, ClientVersionBuild.V8_1_0_28724)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_PARTY, ClientVersionBuild.V8_1_0_28724)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_RAID, ClientVersionBuild.V8_1_0_28724)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_RAID_WARNING, ClientVersionBuild.V8_1_0_28724)]
        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleClientChatMessageInstance(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(10);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49318))
                packet.ReadBit("IsSecure");
            packet.ReadWoWString("Text", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_WHISPER, ClientVersionBuild.V8_1_0_28724)]
        public static void HandleClientChatMessageWhisper(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var recvName = packet.ReadBits(9);
            var msgBitsLen = 10;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479))
                msgBitsLen = 11;
            var msgLen = packet.ReadBits(msgBitsLen);

            packet.ReadWoWString("Target", recvName);
            packet.ReadWoWString("Text", msgLen);
        }
    }
}
