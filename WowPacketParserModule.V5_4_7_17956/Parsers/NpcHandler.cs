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

namespace WowPacketParserModule.V5_4_7_17956.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.CMSG_GOSSIP_HELLO, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleNpcHello547(Packet packet)
        {
            var guid = packet.StartBitStream(6, 3, 4, 5, 1, 7, 2, 0);
            packet.ParseBitStream(guid, 6, 0, 1, 7, 2, 5, 4, 3);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION, ClientVersionBuild.V5_4_7_17956)]
        public static void HandleNpcGossipSelectOption547(Packet packet)
        {
            var guid = new byte[8];

            var menuEntry = packet.ReadUInt32("Menu Id");
            var gossipId = packet.ReadUInt32("Gossip Id");

            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();

            var boxTextLength = packet.ReadBits(8);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);

            packet.ReadWoWString("Box Text", boxTextLength);

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);

            packet.WriteGuid("NPC Guid", guid);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_LIST_INVENTORY, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_LIST_INVENTORY)]
        public static void HandleNpcListInventory547(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 6, 3, 5, 4, 7, 2);
            packet.ParseBitStream(guid, 0, 5, 6, 7, 1, 3, 4, 2);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.CMSG_TRAINER_BUY_SPELL)]
        public static void HandleTrainerBuySpell547(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell Id");
            packet.ReadUInt32("Trainer Id");

            var guid = packet.StartBitStream(6, 2, 0, 7, 5, 3, 1, 4);
            packet.ParseBitStream(guid, 6, 0, 5, 1, 7, 4, 2, 3);

            packet.WriteGuid("NPC Guid", guid);
        }

        [Parser(Opcode.SMSG_SHOW_BANK, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_7_17956)]
        [Parser(Opcode.SMSG_SHOW_BANK)]
        public static void HandleShowBank547(Packet packet)
        {
            var guid = packet.StartBitStream(7, 1, 6, 4, 3, 5, 0, 2);
            packet.ParseBitStream(guid, 6, 0, 7, 3, 5, 1, 4, 2);

            packet.WriteGuid("NPC Guid", guid);
        }
    }
}
