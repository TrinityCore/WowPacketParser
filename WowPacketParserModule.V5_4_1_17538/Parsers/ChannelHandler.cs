using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHAT_MESSAGE_CHANNEL)]
        public static void HandleClientChatMessageChannel(Packet packet)
        {
            packet.ReadInt32E<Language>("Language");
            var channelNameLen = packet.ReadBits(9);
            var msgLen = packet.ReadBits(8);

            packet.ReadWoWString("Channel Name", channelNameLen);
            packet.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            packet.ReadUInt32("Flags");
            packet.ReadBit();
            var length = packet.ReadBits(7);
            packet.ReadBits(7);
            packet.ReadBit("HasPassword");

            packet.ReadWoWString("Channel Name", length);
        }
    }
}