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
            packet.Translator.ReadUInt32("Flags");
            packet.Translator.ReadBit();
            packet.Translator.ReadBits(7);
            packet.Translator.ReadBit();
            var length = packet.Translator.ReadBits(7);

            packet.Translator.ReadWoWString("Channel Name", length);
        }
    }
}