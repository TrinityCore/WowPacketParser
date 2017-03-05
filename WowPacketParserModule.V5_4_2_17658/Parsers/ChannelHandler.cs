using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHAT_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            packet.ReadUInt32("Flags");
            packet.ReadBit();
            packet.ReadBits(7);
            packet.ReadBit();
            var length = packet.ReadBits(7);

            packet.ReadWoWString("Channel Name", length);
        }
    }
}