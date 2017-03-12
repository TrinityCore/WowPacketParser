using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatAddonMessageOfficer
    {
        public string Text;
        public string Prefix;

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_OFFICER, ClientVersionBuild.V4_3_3_15354, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleClientChatMessageAddonOfficer434(Packet packet)
        {
            var length1 = packet.ReadBits(5);
            var length2 = packet.ReadBits(9);
            packet.ReadWoWString("Prefix", length1);
            packet.ReadWoWString("Message", length2);
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_OFFICER, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleAddonMessage602(Packet packet)
        {
            var prefixLen = packet.ReadBits(5);
            var testLen = packet.ReadBits(8);

            packet.ReadWoWString("Prefix", prefixLen);
            packet.ReadWoWString("Text", testLen);
        }
    }
}
