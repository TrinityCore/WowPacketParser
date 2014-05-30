using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using Guid = WowPacketParser.Misc.Guid;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class SessionHandler
    {
        [Parser(Opcode.SMSG_LOGIN_VERIFY_WORLD)]
        public static void HandleLoginVerifyWorld(Packet packet)
        {
            packet.ReadSingle("X");
            packet.ReadSingle("O");
            packet.ReadSingle("Y");
            packet.ReadUInt32("Map");
            packet.ReadSingle("Z");
        }
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            packet.ReadSingle("X");
            packet.ReadUInt32("Map");
            packet.ReadSingle("Y");
            packet.ReadSingle("Z");
            packet.ReadSingle("O");
        }
        [Parser(Opcode.SMSG_TRANSFER_PENDING)]
        public static void HandleTransferPending(Packet packet)
        {
            var unkbit = packet.ReadBit("unk");
            var isTransport = packet.ReadBit("IsTransport");
            packet.ReadUInt32("Map");

            if (isTransport)
            {
                packet.ReadUInt32("MapID");
                packet.ReadUInt32("TransportID");
            }
        }
    }
}
