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

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class NpcHandler
    {
        [Parser(Opcode.CMSG_GOSSIP_HELLO)]
        public static void HandleGossipHello(Packet packet)
        {
            var guid = new byte[8];
            packet.StartBitStream(guid, 5, 2, 0, 4, 7, 1, 6, 3);
            packet.ParseBitStream(guid, 3, 4, 6, 1, 0, 2, 7, 5);

            packet.WriteGuid("GUID", guid);
        }
        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            var gossip = new Gossip();

            var guid = new byte[8];
            
            uint[] titleLen;
            uint[] BoxTextLen;
            uint[] OptionTextLen;

            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            var questgossips = (int)packet.ReadBits(19);

            guid[4] = packet.ReadBit();
            
            titleLen = new uint[questgossips];
            for (var i = 0; i < questgossips; ++i)
            {
                titleLen[i] = packet.ReadBits(9);
                packet.ReadBit("Change Icon", i);
            }

            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            var AmountOfOptions = packet.ReadBits(20);
            
            BoxTextLen = new uint[AmountOfOptions];
            OptionTextLen = new uint[AmountOfOptions];
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                BoxTextLen[i] = packet.ReadBits(12);
                OptionTextLen[i] = packet.ReadBits(12);
            }

            guid[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ReadXORByte(guid, 2);
            
            gossip.GossipOptions = new List<GossipOption>((int)AmountOfOptions);
            for (var i = 0; i < AmountOfOptions; ++i)
            {
                var gossipOption = new GossipOption
                {
                    BoxText = packet.ReadWoWString("Box Text", BoxTextLen[i], i),
                    OptionText = packet.ReadWoWString("Text", OptionTextLen[i], i),
                    Index = packet.ReadUInt32("Index", i),
                    Box = packet.ReadBoolean("Box", i),
                    OptionIcon = packet.ReadEnum<GossipOptionIcon>("Icon", TypeCode.Byte, i),
                    RequiredMoney = packet.ReadUInt32("Required money", i),
                };
            }
            
            for (var i = 0; i < questgossips; ++i)
            {
                packet.ReadInt32("Level", i);
                packet.ReadEnum<QuestFlags>("Flags", TypeCode.UInt32, i);
                packet.ReadUInt32("Icon", i);
                packet.ReadWoWString("Title", titleLen[i], i);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Quest, "Quest ID", i);
                packet.ReadEnum<QuestFlags2>("Flags 2", TypeCode.UInt32, i);
            }

            packet.ReadXORByte(guid, 7);

            packet.ReadUInt32("Friendship Faction");

            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            
            var textId = packet.ReadUInt32("Text Id");

            packet.ReadXORByte(guid, 5);
            
            var menuId = packet.ReadUInt32("Menu Id");

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);

            var GUID = new Guid(BitConverter.ToUInt64(guid, 0));
            gossip.ObjectType = GUID.GetObjectType();
            gossip.ObjectEntry = GUID.GetEntry();

            Storage.Gossips.Add(Tuple.Create(menuId, textId), gossip, packet.TimeSpan);
            packet.AddSniffData(StoreNameType.Gossip, (int)menuId, GUID.GetEntry().ToString(CultureInfo.InvariantCulture));
        }

        [Parser(Opcode.CMSG_NPC_TEXT_QUERY)]
        public static void HandleNpcTextQuery(Packet packet)
        {
            var entry = packet.ReadInt32("Entry");

            var guid = new byte[8];
            packet.StartBitStream(guid, 5, 6, 7, 4, 3, 0, 2, 1);
            packet.ParseBitStream(guid, 0, 7, 1, 4, 3, 5, 2, 6);
            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_NPC_TEXT_UPDATE)]
        public static void HandleNpcTextUpdate(Packet packet)
        {
            var npcText = new NpcText();

            var hasData = packet.ReadBit();
            if (!hasData)
                return; // nothing to do

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // Can be masked
                return;

            var size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);

            var pkt = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);
            npcText.Probabilities = new float[8];
            for (var i = 0; i < 8; ++i)
                npcText.Probabilities[i] = pkt.ReadSingle("Probability", i);
            for (var i = 0; i < 8; ++i)
                pkt.ReadInt32("Broadcast Text Id", i);
        }
               
    }
}
