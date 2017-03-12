using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatAddonMessageInstanceChat
    {
        public string Prefix;
        public string Text;

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_INSTANCE_CHAT, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAddonMessage602(Packet packet)
        {
            var prefixLen = packet.ReadBits(5);
            var testLen = packet.ReadBits(8);

            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Text", testLen);
        }
    }
}
