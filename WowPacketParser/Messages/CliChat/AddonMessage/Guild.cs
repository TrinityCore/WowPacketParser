using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.CliChat.AddonMessage
{
    public unsafe struct Guild
    {
        public string Prefix;
        public string Text;

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_GUILD, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleClientChatMessageAddon(Packet packet)
        {
            packet.ReadCString("Message");
            packet.ReadCString("Prefix");
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_GUILD, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientChatMessageAddon434(Packet packet)
        {
            var length1 = packet.ReadBits(9);
            var length2 = packet.ReadBits(5);
            packet.ReadWoWString("Message", length1);
            packet.ReadWoWString("Prefix", length2);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_GUILD, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAddonMessage602(Packet packet)
        {
            var prefixLen = packet.ReadBits(5);
            var testLen = packet.ReadBits(8);

            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Text", testLen);
        }
    }
}
