using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var GUID = new byte[8];
            GUID = packet.StartBitStream(2, 4, 0, 3, 6, 7, 5, 1);
            packet.ParseBitStream(GUID, 4, 7, 1, 0, 5, 3, 6, 2);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        public static void HandleBankerActivate(Packet packet)
        {
            var GUID = new byte[8];
            GUID = packet.StartBitStream(4, 5, 0, 6, 1, 2, 7, 3);
            packet.ParseBitStream(GUID, 1, 7, 2, 5, 6, 3, 0, 4);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleShowBank(Packet packet)
        {
            var GUID = new byte[8];
            GUID = packet.StartBitStream(2, 4, 3, 6, 5, 1, 7, 0);
            packet.ParseBitStream(GUID, 7, 0, 5, 3, 6, 1, 4, 2);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.CMSG_AUCTION_HELLO)]
        public static void HandleAuctionHello(Packet packet)
        {
            var GUID = new byte[8];
            GUID = packet.StartBitStream(1, 5, 2, 0, 3, 6, 4, 7);
            packet.ParseBitStream(GUID, 2, 7, 1, 3, 5, 0, 4, 6);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var GUID = new byte[8];
            GUID = packet.StartBitStream(7, 6, 5, 4, 3, 2, 1, 0);
            packet.ParseBitStream(GUID, 0, 7, 3, 5, 1, 4, 6, 2);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.SMSG_AUCTION_HELLO)]
        public static void HandleAuctionHelloResponse(Packet packet)
        {
            var GUID = new byte[8];
            GUID[6] = packet.ReadBit();
            GUID[7] = packet.ReadBit();
            GUID[3] = packet.ReadBit();
            var inUse = packet.ReadBit("inUse");
            GUID[4] = packet.ReadBit();
            GUID[2] = packet.ReadBit();
            GUID[5] = packet.ReadBit();
            GUID[0] = packet.ReadBit();
            GUID[1] = packet.ReadBit();

            packet.ReadXORByte(GUID, 3);
            var AHID = packet.ReadUInt32("Entry: ");
            packet.ParseBitStream(GUID, 4, 7, 1, 0, 3, 5);
            packet.WriteGuid("GUID", GUID);
        }
    }
}
