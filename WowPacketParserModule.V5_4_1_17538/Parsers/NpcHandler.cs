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

namespace WowPacketParserModule.V5_4_1_17359.Parsers
{
    public static class NpcHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var guid = new byte[8];
            uint[] titleLen;
            uint[] OptionTextLen;
            uint[] BoxTextLen;

            var AmountOfOptions = packet.ReadBits(20);
            packet.StartBitStream(guid, 5, 1, 7, 2);
            var questgossips = packet.ReadBits(19);
            packet.StartBitStream(guid, 6, 4, 0);

            titleLen = new uint[questgossips];
            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadBit("Change Icon", i);
                titleLen[i] = packet.ReadBits(9);
            }

            OptionTextLen = new uint[AmountOfOptions];
            BoxTextLen = new uint[AmountOfOptions];
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                BoxTextLen[i] = packet.ReadBits(12);
                OptionTextLen[i] = packet.ReadBits(12);
            }

            guid[3] = packet.ReadBit();
            for (var i = 0; i < questgossips; i++)
            {
                packet.ReadEnum<QuestFlags>("Flags", TypeCode.UInt32, i);
                packet.ReadUInt32("Icon", i);
                packet.ReadInt32("Level", i);
                packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID", i);
                packet.ReadEnum<QuestFlags2>("Flags 2", TypeCode.UInt32, i);
            }

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);

            var menuId = packet.ReadUInt32("Menu Id");

            var gossip = new Gossip();

            gossip.GossipOptions = new List<GossipOption>((int)AmountOfOptions);
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                var gossipOption = new GossipOption
                {
                    OptionText = packet.ReadWoWString("Text", OptionTextLen[i], i),
                    Box = packet.ReadBoolean("Box", i),
                    OptionIcon = packet.ReadEnum<GossipOptionIcon>("Icon", TypeCode.Byte, i),
                    RequiredMoney = packet.ReadUInt32("Required money", i),
                    BoxText = packet.ReadWoWString("Box Text", BoxTextLen[i], i),
                    Index = packet.ReadUInt32("Index", i),
                };

                gossip.GossipOptions.Add(gossipOption);
            }

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 6);
            packet.ReadUInt32("Friendship Faction");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            var textId = packet.ReadUInt32("Text Id");
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);

            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));
            gossip.ObjectType = GUID.GetObjectType();
            gossip.ObjectEntry = GUID.GetEntry();

            Storage.Gossips.Add(Tuple.Create(menuId, textId), gossip, packet.TimeSpan);
            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, GUID.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcText();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            npcText.Probabilities = new float[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                pkt.ReadInt32("Broadcast Text Id", i);
        }

        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 2, 0, 1, 5, 7, 6, 4, 3);
            packet.ParseBitStream(guid, 6, 3, 2, 0, 5, 1, 7, 4);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");

            var guid = new byte[8];
            packet.StartBitStream(guid, 4, 7, 2, 5, 3, 0, 1, 6);
            packet.ParseBitStream(guid, 6, 4, 1, 3, 2, 5, 7, 0);
            packet.WriteGuid("GUID", guid);
        }

        [Parser(Opcode.CMSG_GOSSIP_SELECT_OPTION)]
        public static void HandleNpcGossipSelectOption(Packet packet)
        {
            var guid = new byte[8];

            var gossipId = packet.ReadUInt32("Gossip Id");
            var menuEntry = packet.ReadUInt32("Menu Id");

            packet.StartBitStream(guid, 4, 0, 6, 3, 2, 7, 1);

            var bits8 = packet.ReadBits(8);

            guid[5] = packet.ReadBit();

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);

            packet.ReadWoWString("Box Text", bits8);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);

            Storage.GossipSelects.Add(Tuple.Create(menuEntry, gossipId), null, packet.TimeSpan);
            packet.WriteGuid("GUID", guid);
        }
    }
}
