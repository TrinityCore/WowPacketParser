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

namespace WowPacketParserModule.V5_4_1_17538.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.CMSG_BANKER_ACTIVATE)]
        public static void HandleBankerActivate(Packet packet)
        {
            var guid = packet.StartBitStream(1, 4, 2, 7, 5, 0, 3, 6);
            packet.ParseBitStream(guid, 4, 2, 5, 7, 0, 6, 3, 1);
            packet.WriteGuid("NPC Guid", guid);
        }
        
        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleShowBank(Packet packet)
        {
            var guid = packet.StartBitStream(6, 0, 4, 3, 2, 1, 7, 5);
            packet.ParseBitStream(guid, 7, 2, 0, 1, 6, 5, 3, 4);
            packet.WriteGuid("NPC Guid", guid);
        }
        
        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = packet.StartBitStream(2, 0, 1, 5, 7, 6, 4, 3);
            packet.ParseBitStream(guid, 6, 3, 2, 0, 5, 1, 7, 4);
            packet.WriteGuid("NPC Guid", guid);
        }
    }
}
