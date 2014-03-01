using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class ChannelHandler
    {
        [Parser(Opcode.CMSG_CHANNEL_LIST)]
        public static void HandleChannelList(Packet packet)
        {
            packet.ReadUInt32("Flags");
            var pwLen = packet.ReadBits(7);
            packet.ReadBit();
            var nameLen = packet.ReadBits(7);
            packet.ReadBit();

            packet.ReadWoWString("Password", pwLen);
            packet.ReadWoWString("Channel Name", nameLen);
        }
    }
}