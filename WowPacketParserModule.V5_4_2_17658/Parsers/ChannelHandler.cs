using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class ChannelHandler
    {

        [Parser(Opcode.CMSG_CHANNEL_LIST)]
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