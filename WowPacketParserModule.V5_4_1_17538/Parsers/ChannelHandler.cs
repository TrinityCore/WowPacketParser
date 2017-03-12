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
    }
}