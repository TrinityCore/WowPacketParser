using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V2_5_1_38707.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHAT_JOIN_CHANNEL)]
        public static void HandleChannelJoin(Packet packet)
        {
            packet.ReadInt32("ChatChannelId");

            var channelLength = packet.ReadBits(7);
            var passwordLength = packet.ReadBits(7);

            packet.ReadWoWString("ChannelName", channelLength);
            packet.ReadWoWString("Password", passwordLength);
        }
    }
}
