using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.CliChat.Message
{
    public unsafe struct Channel
    {
        public int Language;
        public string Text;
        public string Target;

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleClientChatMessageChannel(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            packet.ReadCString("Message");
            packet.ReadCString("Channel Name");
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleClientChatMessageChannel434(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var channelNameLen = packet.ReadBits(10);
            var msgLen = packet.ReadBits(9);

            packet.ReadWoWString("Message", msgLen);
            packet.ReadWoWString("Channel Name", channelNameLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_1_17538)]
        public static void HandleClientChatMessageChannel530(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var msgLen = packet.ReadBits(8);
            var channelNameLen = packet.ReadBits(9);

            packet.ReadWoWString("Message", msgLen);
            packet.ReadWoWString("Channel Name", channelNameLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL, ClientVersionBuild.V5_4_1_17538, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleClientChatMessageChannel541(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var channelNameLen = packet.ReadBits(9);
            var msgLen = packet.ReadBits(8);

            packet.ReadWoWString("Channel Name", channelNameLen);
            packet.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleClientChatMessageChannel547(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var msgLen = packet.ReadBits(8);
            var channelNameLen = packet.ReadBits(9);

            packet.ReadWoWString("Channel Name", channelNameLen);
            packet.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientChatMessageChannel548(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var channelNameLen = packet.ReadBits(9);
            var msgLen = packet.ReadBits(8);
            packet.ReadWoWString("Message", msgLen);
            packet.ReadWoWString("Channel Name", channelNameLen);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientChatMessageChannel602(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var channelNameLen = packet.ReadBits(9);
            var msgLen = packet.ReadBits(8);
            packet.ResetBitReader();
            packet.ReadWoWString("Channel Name", channelNameLen);
            packet.ReadWoWString("Message", msgLen);
        }
    }
}
