using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.CliChat.Message
{
    public unsafe struct Emote
    {
        public string Text;

        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleClientChatMessageEmote(Packet packet)
        {
            packet.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE, ClientVersionBuild.V4_3_4_15595, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleClientChatMessageEmote434(Packet packet)
        {
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleClientChatMessage530(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleMessageChatDND547(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleMessageChatDND548(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_EMOTE, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleMessageChatDND(Packet packet)
        {
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }
    }
}
