using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.CliChat.AddonMessage
{
    public unsafe struct Whisper
    {
        public string Prefix;
        public string Target;
        public string Text;

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_WHISPER, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleClientChatMessageAddonWhisper(Packet packet)
        {
            packet.ReadCString("Prefix");
            packet.ReadCString("Target Name");
            packet.ReadCString("Message");
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_WHISPER, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientChatMessageAddonWhisper434(Packet packet)
        {
            var msgLen = packet.ReadBits(9);
            var prefixLen = packet.ReadBits(5);
            var targetLen = packet.ReadBits(10);
            packet.ReadWoWString("Message", msgLen);
            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Target Name", targetLen);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_WHISPER, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAddonMessageWhisper(Packet packet)
        {
            var targetLen = packet.ReadBits(9);
            var prefixLen = packet.ReadBits(5);
            var testLen = packet.ReadBits(8);

            packet.ReadWoWString("Target", targetLen);
            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Text", testLen);
        }
    }
}
