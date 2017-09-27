using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.CliChat.AddonMessage
{
    public unsafe struct Battleground // not confirmed
    {
        public string Text;
        public string Prefix;

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_BATTLEGROUND, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleClientChatMessageAddon(Packet packet)
        {
            packet.ReadCString("Message");
            packet.ReadCString("Prefix");
        }

        [Parser(Opcode.CMSG_CHAT_ADDON_MESSAGE_BATTLEGROUND, ClientVersionBuild.V4_3_3_15354)]
        public static void HandleClientChatMessageAddon434(Packet packet)
        {
            var length1 = packet.ReadBits(9);
            var length2 = packet.ReadBits(5);
            packet.ReadWoWString("Message", length1);
            packet.ReadWoWString("Prefix", length2);
        }
    }
}
