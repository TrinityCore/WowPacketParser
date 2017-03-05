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
            packet.Translator.ReadInt32E<Language>("Language");
            var channelNameLen = packet.Translator.ReadBits(9);
            var msgLen = packet.Translator.ReadBits(8);

            packet.Translator.ReadWoWString("Channel Name", channelNameLen);
            packet.Translator.ReadWoWString("Message", msgLen);
        }

        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            packet.Translator.ReadUInt32("Flags");
            packet.Translator.ReadBit();
            var length = packet.Translator.ReadBits(7);
            packet.Translator.ReadBits(7);
            packet.Translator.ReadBit("HasPassword");

            packet.Translator.ReadWoWString("Channel Name", length);
        }
    }
}