using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatMessageInstanceChat
    {
        public int Language;
        public string Text;

        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleClientChatMessage530(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_1_17538)]
        public static void HandleClientChatMessage540(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT, ClientVersionBuild.V5_4_1_17538, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleClientChatMessage541(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleClientChatMessage542(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleClientChatMessage547(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Message", len);
        }

        [Parser(Opcode.CMSG_CHAT_MESSAGE_INSTANCE_CHAT, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientChatMessage602(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var len = packet.ReadBits(8);
            packet.ReadWoWString("Text", len);
        }
    }
}
